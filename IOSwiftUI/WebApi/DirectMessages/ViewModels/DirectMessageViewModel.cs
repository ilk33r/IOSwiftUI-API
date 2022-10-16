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
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.DirectMessages.ViewModels;

public class DirectMessageViewModel : ViewModel
{

    public IList<InboxModel> GetInboxes()
    {
        List<InboxModel> inboxes = DBContext.Inbox
                                            .Select(i => new InboxModel()
                                            {
                                                InboxID = i.ID,
                                                FromMemberID = i.FromMember.ID,
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
        MemberEntity toMember = DBContext.Members
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
            
        DBContext.Attach(fromMember);
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

    public void SendMessage(int toMemberID, string message)
    {
        MemberEntity fromMember = new MemberEntity()
        {
            ID = CurrentMember.ID
        };

        MemberEntity toMember = new MemberEntity()
        {
            ID = toMemberID
        };

        DBContext.Attach(fromMember);
        DBContext.Attach(toMember);

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

        DBContext.Add(fromMemberMessage);

        fromMemberConversation.UnreadMessageCount = 0;
        fromMemberConversation.LastMessage = fromMemberMessage;
        fromMemberConversation.UpdateDate = fromMemberMessage.MessageDate;
        DBContext.Update(fromMemberConversation);

        MessageEntity toMemberMessage = new MessageEntity()
        {
            InboxID = toMemberConversation.ID,
            FromMember = toMember,
            ToMember = fromMember,
            Message = decryptedMessage,
            MessageDate = DateTimeOffset.UtcNow
        };

        DBContext.Add(toMemberMessage);

        toMemberConversation.UnreadMessageCount += 1;
        toMemberConversation.LastMessage = toMemberMessage;
        toMemberConversation.UpdateDate = toMemberMessage.MessageDate;
        DBContext.Update(toMemberConversation);

        DBContext.SaveChanges();
    }

    public void DeleteInbox(int inboxID)
    {
        InboxEntity inbox = DBContext.Inbox
                                        .Include(i => i.Messages)
                                        .Where(i => i.ID == inboxID)
                                        .FirstOrDefault();

        if (inbox == null)
        {
            throw new InboxNotFoundException();
        }

        inbox.LastMessage = null;
        DBContext.SaveChanges();
        
        foreach (MessageEntity message in inbox.Messages)
        {
            DBContext.Remove(message);
        }

        DBContext.Remove(inbox);
        DBContext.SaveChanges();
    }

    public GetMessagesResponseModel GetMessages(PaginationModel pagination, int inboxID)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DBContext.DirectMessages
                                                .Where(dm => dm.InboxID == inboxID)
                                                .Count();

        List<MessageModel> memberMessages = DBContext.DirectMessages
                                                        .Select(dm => new MessageModel()
                                                        {
                                                            InboxID = dm.InboxID,
                                                            MessageID = dm.ID,
                                                            Message = dm.Message,
                                                            MessageDate = dm.MessageDate
                                                        })
                                                        .Where(dm => dm.InboxID == inboxID)
                                                        .OrderByDescending(dm => dm.MessageDate)
                                                        .Skip(pagination.Start)
                                                        .Take(pagination.Count)
                                                        .ToList();

        foreach(MessageModel message in memberMessages)
        {
            message.Message = EncryptString(message.Message);
        }

        responsePagination.Count = memberMessages.Count();
        return new GetMessagesResponseModel(memberMessages, responsePagination);
    }

    private InboxEntity CreateConversation(MemberEntity fromMember, MemberEntity toMember)
    {
        InboxEntity inbox = DBContext.Inbox
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

        DBContext.Entry(fromMember).State = EntityState.Unchanged;
        DBContext.Entry(toMember).State = EntityState.Unchanged;

        DBContext.Add(inbox);
        DBContext.SaveChanges();
            
        return inbox;
    }
}
