using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberRegisterViewModel : ViewModel
{
    public void CheckMember(string email)
    {
        MemberEntity member = DBContext.Members.Where(m => m.Email.ToLower().Equals(email.ToLower()))
                                .FirstOrDefault();

        if (member != null)
        {
            throw new IOUserExistsException();
        }
    }
}
