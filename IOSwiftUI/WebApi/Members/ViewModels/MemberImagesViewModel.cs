using System;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberImagesViewModel : ImageViewModel
{

    public void AddMemberImage(string fileName)
    {
        MemberEntity member = new MemberEntity()
        {
            ID = CurrentMember.ID
        };
        DBContext.Attach(member);

        ImagesEntity memberImage = new ImagesEntity()
        {
            FileName = fileName,
            CreateDate = DateTime.UtcNow,
            Member = member
        };

        DBContext.Add(memberImage);
        DBContext.SaveChanges();
    }

    public void UpdateMemberProfilePicture(string fileName)
    {
        MemberEntity member = DBContext.Members.Find(CurrentMember.ID);
        if (member == null)
        {
            RemoveFile(fileName);
            throw new IOUserNotFoundException();
        }

        member.ProfilePictureFileName = fileName;
        DBContext.Update(member);
        DBContext.SaveChanges();
    }
}
