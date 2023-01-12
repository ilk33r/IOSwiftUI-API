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
        MemberEntity member = DatabaseContext.Members.Where(m => m.Email.ToLower().Equals(email.ToLower()))
                                .FirstOrDefault();

        if (member != null)
        {
            throw new IOUserExistsException();
        }
    }

    public void CheckMemberUserName(string userName)
    {
        MemberEntity member = DatabaseContext.Members.Where(m => m.UserName.ToLower().Equals(userName.ToLower()))
                                .FirstOrDefault();

        if (member != null)
        {
            throw new IOUserExistsException();
        }
    }

    public void PairFaceID(string encrypredAuthenticationKey)
    {
        string authenticationKey = DecryptString(encrypredAuthenticationKey);
        
        MemberFaceIDEntity faceIDEntity = DatabaseContext.MemberFaceIDs.Where(fid => fid.Member.ID == CurrentMember.ID)
                                                                    .FirstOrDefault();

        if (faceIDEntity == null)
        {
            MemberEntity currentMember = DatabaseContext.Members.Find(CurrentMember.ID);
            faceIDEntity = new MemberFaceIDEntity()
            {
                Member = currentMember,
                AuthenticationKey = authenticationKey,
                PairDate = DateTimeOffset.UtcNow
            };

            DatabaseContext.Add(faceIDEntity);
        }
        else
        {
            faceIDEntity.AuthenticationKey = authenticationKey;
            DatabaseContext.Update(faceIDEntity);
        }
        
        DatabaseContext.SaveChanges();
    }

    public void RegisterMember(RegisterMemberRequestModel requestModel)
    {
        CheckOTPValidated(requestModel.PhoneNumber);
        CheckMember(requestModel.Email);
        CheckMemberUserName(requestModel.UserName);

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
            PhoneNumber = requestModel.PhoneNumber,
            UserStatus = UserStatuses.Active,
            DeviceId = requestModel.DeviceId,
            DeviceManifacturer = requestModel.DeviceManifacturer,
            DeviceModel = requestModel.DeviceModel
        };

        DatabaseContext.Members.Add(newMember);
        DatabaseContext.SaveChanges();
    }
}
