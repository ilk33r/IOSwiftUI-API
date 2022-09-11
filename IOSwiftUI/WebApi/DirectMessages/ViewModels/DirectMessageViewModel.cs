using System;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Common.Models.DirectMessages;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.WebApi.DirectMessages.ViewModels;

public class DirectMessageViewModel : ViewModel
{
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
