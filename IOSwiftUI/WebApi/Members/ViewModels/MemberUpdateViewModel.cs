using System;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Messages.Members;
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

    public void UpdateMember(RegisterMemberRequestModel requestModel)
    {
        MemberEntity member = DBContext.Members
                                        .Find(CurrentMember.ID);

        if (member == null)
        {
            throw new IOUserNotFoundException();
        }

        member.BirthDate = requestModel.BirthDate;
        member.Name = requestModel.Name;
        member.Surname = requestModel.Surname;
        member.LocationName = requestModel.LocationName;
        member.LocationLatitude = requestModel.LocationLatitude;
        member.LocationLongitude = requestModel.LocationLongitude;
        member.PhoneNumber = requestModel.PhoneNumber;
        member.DeviceId = requestModel.DeviceId;
        member.DeviceManifacturer = requestModel.DeviceManifacturer;
        member.DeviceModel = requestModel.DeviceModel;

        DBContext.Update(member);
        DBContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, CurrentMember.ID);
        IOCache.InvalidateCache(cacheKey);
    }
}
