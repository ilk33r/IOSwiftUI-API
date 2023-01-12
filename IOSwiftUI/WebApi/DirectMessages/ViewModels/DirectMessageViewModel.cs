using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Messages.DirectMessages;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.DirectMessages;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.DirectMessages.ViewModels;

public class DirectMessageViewModel : ViewModel
{

    public IList<InboxModel> GetInboxes()
    {
        List<InboxModel> inboxes = DatabaseContext.Inbox
                                            .Select(i => new InboxModel()
                                            {
                                                InboxID = i.ID,
                                                FromMemberID = i.FromMember.ID,
                                                ToMemberID = i.ToMember.ID,
                                                UserName = i.ToMember.UserName,
                                                NameSurname = i.ToMember.Name + " " + i.ToMember.Surname,
                                                ProfilePicturePublicID = i.ToMember.ProfilePictureFileName,
                                                UpdateDate = i.UpdateDate,
                                                UnreadMessageCount = i.UnreadMessageCount,
                                                LastMessage = i.LastMessage == null ? null : new MessageModel()
                                                {
                                                    MessageID = i.LastMessage.ID,
                                                    Message = i.LastMessage.Message,
                                                    MessageDate = i.LastMessage.MessageDate
                                                }
                                            })
                                            .Where(i => i.FromMemberID == CurrentMember.ID)
                                            .ToList();

        foreach(InboxModel inbox in inboxes)
        {
            if (!String.IsNullOrEmpty(inbox.ProfilePicturePublicID))
            {
                inbox.ProfilePicturePublicID = CreatePublicId(inbox.ProfilePicturePublicID);
            }

            if (Configuration.GetValue<bool>(ConfigurationConstants.PasswordDecryptionEnabledKey))
            {
                if (inbox.LastMessage != null && !String.IsNullOrEmpty(inbox.LastMessage.Message))
                {
                    inbox.LastMessage.Message = EncryptString(inbox.LastMessage.Message);
                }
            }
        }

        return inboxes;
    }

    public InboxModel CreateConversation(int toMemberID)
    {
        MemberEntity toMember = DatabaseContext.Members
                                            .Select(member => new MemberEntity() 
                                            { 
                                                ID = member.ID,
                                                UserName = member.UserName,
                                                Name = member.Name,
                                                Surname = member.Surname,
                                                ProfilePictureFileName = member.ProfilePictureFileName
                                            })
                                            .Where(member => member.ID == toMemberID)
                                            .FirstOrDefault();

        if (toMember == null)
        {
            throw new IOUserNotFoundException();
        }

        MemberEntity fromMember = new MemberEntity()
        {
            ID = CurrentMember.ID
        };
            
        DatabaseContext.Attach(fromMember);
        InboxEntity inbox = CreateConversation(fromMember, toMember);
            
        InboxModel response = new InboxModel()
        {
            InboxID = inbox.ID,
            UserName = toMember.UserName,
            NameSurname = toMember.Name + " " + toMember.Surname,
            ProfilePicturePublicID = CreatePublicId(toMember.ProfilePictureFileName),
            UpdateDate = inbox.UpdateDate,
            UnreadMessageCount = inbox.UnreadMessageCount
        };

        return response;
    }

    public MessageModel SendMessage(int toMemberID, string message)
    {
        MemberEntity fromMember = new MemberEntity()
        {
            ID = CurrentMember.ID
        };

        MemberEntity toMember = new MemberEntity()
        {
            ID = toMemberID
        };

        DatabaseContext.Attach(fromMember);
        DatabaseContext.Attach(toMember);

        InboxEntity fromMemberConversation = CreateConversation(fromMember, toMember);
        InboxEntity toMemberConversation = CreateConversation(toMember, fromMember);

        string decryptedMessage;
        if (Configuration.GetValue<bool>(ConfigurationConstants.PasswordDecryptionEnabledKey))
        {
            decryptedMessage = DecryptString(message);
        }
        else
        {
            decryptedMessage = message;
        }

        MessageEntity fromMemberMessage = new MessageEntity()
        {
            InboxID = fromMemberConversation.ID,
            FromMember = fromMember,
            ToMember = toMember,
            Message = decryptedMessage,
            MessageDate = DateTimeOffset.UtcNow
        };

        DatabaseContext.Add(fromMemberMessage);

        fromMemberConversation.UnreadMessageCount = 0;
        fromMemberConversation.LastMessage = fromMemberMessage;
        fromMemberConversation.UpdateDate = fromMemberMessage.MessageDate;
        DatabaseContext.Update(fromMemberConversation);

        MessageEntity toMemberMessage = new MessageEntity()
        {
            InboxID = toMemberConversation.ID,
            FromMember = fromMember,
            ToMember = toMember,
            Message = decryptedMessage,
            MessageDate = DateTimeOffset.UtcNow
        };

        DatabaseContext.Add(toMemberMessage);

        toMemberConversation.UnreadMessageCount += 1;
        toMemberConversation.LastMessage = toMemberMessage;
        toMemberConversation.UpdateDate = toMemberMessage.MessageDate;
        DatabaseContext.Update(toMemberConversation);

        DatabaseContext.SaveChanges();

        MessageModel messageModel = new MessageModel()
        {
            InboxID = fromMemberConversation.ID,
            MessageID = fromMemberMessage.ID,
            MessageDate = fromMemberMessage.MessageDate,
            IsSent = true
        };

        messageModel.Message = EncryptString(decryptedMessage);
        messageModel.UserAvatarPublicID = CurrentMember.ProfilePicturePublicId;

        return messageModel;
    }

    public void DeleteInbox(int inboxID)
    {
        InboxEntity inbox = DatabaseContext.Inbox
                                        .Include(i => i.Messages)
                                        .Where(i => i.ID == inboxID)
                                        .FirstOrDefault();

        if (inbox == null)
        {
            throw new InboxNotFoundException();
        }

        inbox.LastMessage = null;
        DatabaseContext.SaveChanges();
        
        foreach (MessageEntity message in inbox.Messages)
        {
            DatabaseContext.Remove(message);
        }

        DatabaseContext.Remove(inbox);
        DatabaseContext.SaveChanges();
    }

    public GetMessagesResponseModel GetMessages(PaginationModel pagination, int inboxID)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DatabaseContext.DirectMessages
                                                .Where(dm => dm.InboxID == inboxID)
                                                .Count();

        int currentMemberID = CurrentMember.ID;
        List<MessageModel> memberMessages = DatabaseContext.DirectMessages
                                                        .Select(dm => new MessageModel()
                                                        {
                                                            InboxID = dm.InboxID,
                                                            MessageID = dm.ID,
                                                            Message = dm.Message,
                                                            MessageDate = dm.MessageDate,
                                                            IsSent = dm.FromMember.ID == currentMemberID,
                                                            UserAvatarPublicID = dm.FromMember.ProfilePictureFileName
                                                        })
                                                        .Where(dm => dm.InboxID == inboxID)
                                                        .OrderByDescending(dm => dm.MessageDate)
                                                        .Skip(pagination.Start)
                                                        .Take(pagination.Count)
                                                        .ToList();

        foreach(MessageModel message in memberMessages)
        {
            message.Message = EncryptString(message.Message);
            if (!String.IsNullOrEmpty(message.UserAvatarPublicID))
            {
                message.UserAvatarPublicID = CreatePublicId(message.UserAvatarPublicID);
            }
        }

        responsePagination.Count = memberMessages.Count();
        memberMessages.Reverse();
        return new GetMessagesResponseModel(memberMessages, responsePagination);
    }

    private InboxEntity CreateConversation(MemberEntity fromMember, MemberEntity toMember)
    {
        InboxEntity inbox = DatabaseContext.Inbox
                                        .Where(i => i.FromMember.ID == fromMember.ID && i.ToMember.ID == toMember.ID)
                                        .FirstOrDefault();

        if (inbox != null) 
        {
            return inbox;
        }

        inbox = new InboxEntity() 
        {
            FromMember = fromMember,
            ToMember = toMember,
            CreateDate = DateTimeOffset.UtcNow,
            UpdateDate = DateTimeOffset.UtcNow,
            UnreadMessageCount = 0
        };

        EntityEntry<MemberEntity> fromMemberEntity = DatabaseContext.Entry(fromMember);
        EntityEntry<MemberEntity> toMemberEntity = DatabaseContext.Entry(toMember);

        if (fromMemberEntity.State != EntityState.Unchanged)
        {
            fromMemberEntity.State = EntityState.Unchanged;
        }

        if (toMemberEntity.State != EntityState.Unchanged)
        {
            toMemberEntity.State = EntityState.Unchanged;
        }

        DatabaseContext.Add(inbox);
        DatabaseContext.SaveChanges();
            
        return inbox;
    }
}
