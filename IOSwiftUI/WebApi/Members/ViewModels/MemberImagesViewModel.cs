using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberImagesViewModel : ImageViewModel
{
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
