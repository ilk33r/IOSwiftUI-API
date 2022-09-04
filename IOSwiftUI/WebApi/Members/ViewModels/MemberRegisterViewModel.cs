using System;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Messages.Members;
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

    public void RegisterMember(RegisterMemberRequestModel requestModel)
    {
        CheckMember(requestModel.Email);

        string decryptedPassword = DecryptString(requestModel.Password);
        string password = IOPasswordUtilities.HashPassword(decryptedPassword);
        MemberEntity newMember = new MemberEntity()
        {
            UserName = requestModel.UserName,
            Password = password,
            RegisterDate = DateTime.UtcNow,
            BirthDate = requestModel.BirthDate,
            Email = requestModel.Email,
            Name = requestModel.Name,
            Surname = requestModel.Surname,
            LocationName = requestModel.LocationName,
            LocationLatitude = requestModel.LocationLatitude,
            LocationLongitude = requestModel.LocationLongitude,
            UserStatus = UserStatuses.Active
        };

        DBContext.Members.Add(newMember);
        DBContext.SaveChanges();
    }
}
