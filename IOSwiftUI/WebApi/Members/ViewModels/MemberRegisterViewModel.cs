using System;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberRegisterViewModel : ViewModel
{
    private const int OTPValidateTimeout = 360;

    public void CheckMember(string email)
    {
        MemberEntity member = DBContext.Members.Where(m => m.Email.ToLower().Equals(email.ToLower()))
                                .FirstOrDefault();

        if (member != null)
        {
            throw new IOUserExistsException();
        }
    }

    public void CheckMemberUserName(string userName)
    {
        MemberEntity member = DBContext.Members.Where(m => m.UserName.ToLower().Equals(userName.ToLower()))
                                .FirstOrDefault();

        if (member != null)
        {
            throw new IOUserExistsException();
        }
    }

    public int SendOTP(string phoneNumber)
    {
        int otpTimeout = Configuration.GetValue<int>(ConfigurationConstants.OTPTimeout);
        
        OneTimeCodeEntity otpEntity = DBContext.OneTimeCodes.Where(otp => otp.PhoneNumber.Equals(phoneNumber))
                                                .FirstOrDefault();

        if (otpEntity == null)
        {
            otpEntity = new OneTimeCodeEntity()
            {
                PhoneNumber = phoneNumber,
                OneTimeCode = IORandomUtilities.GenerateRandomNumericString(6),
                CreateDate = DateTimeOffset.UtcNow,
                IsValidated = false
            };

            DBContext.Add(otpEntity);
            DBContext.SaveChanges();

            return otpTimeout;
        }

        long createDateUnixTime = otpEntity.CreateDate.ToUnixTimeSeconds();
        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        DateTimeOffset validateDate = otpEntity.ValidateDate ?? DateTimeOffset.UtcNow.AddHours(-1);
        if (otpEntity.IsValidated && currentUnixTime < (validateDate.ToUnixTimeSeconds() + OTPValidateTimeout))
        {
            throw new OTPFraudException();
        }
        else if (currentUnixTime < (createDateUnixTime + otpTimeout))
        {
            throw new OTPAlreadySentException();
        }

        otpEntity.CreateDate = DateTimeOffset.UtcNow;
        otpEntity.ValidateDate = null;
        otpEntity.IsValidated = false;

        return otpTimeout;
    }

    public void RegisterMember(RegisterMemberRequestModel requestModel)
    {
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
            UserStatus = UserStatuses.Active
        };

        DBContext.Members.Add(newMember);
        DBContext.SaveChanges();
    }
}
