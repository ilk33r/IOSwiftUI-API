using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberUpdateViewModel : ViewModel
{
    public void ChangePassword(string oldPassword, string newPassword)
    {
        string decryptedOldPassword = DecryptString(oldPassword);
        string decryptedNewPassword = DecryptString(newPassword);

        MemberEntity member = DBContext.Members
                                        .Find(CurrentMember.ID);

        if (member == null)
        {
            throw new IOUserNotFoundException();
        }

        CheckOTPValidated(member.PhoneNumber);

        if (IOPasswordUtilities.VerifyPassword(decryptedOldPassword, member.Password))
        {
            throw new IOInvalidCredentialsException();
        }

        member.Password = IOPasswordUtilities.HashPassword(decryptedNewPassword);
        DBContext.Update(member);
        DBContext.SaveChanges();
    }
}
