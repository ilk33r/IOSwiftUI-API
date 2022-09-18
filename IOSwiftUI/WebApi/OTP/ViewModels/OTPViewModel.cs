using System;
using System.Linq;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.OTP.ViewModels;

public class OTPViewModel : ViewModel
{
    public int Send(string phoneNumber)
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
        int otpValidateTimeout = Configuration.GetValue<int>(ConfigurationConstants.OTPValidateTimeout);
        DateTimeOffset validateDate = otpEntity.ValidateDate ?? DateTimeOffset.UtcNow.AddHours(-1);

        if (otpEntity.IsValidated && currentUnixTime < (validateDate.ToUnixTimeSeconds() + otpValidateTimeout))
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
        otpEntity.OneTimeCode = IORandomUtilities.GenerateRandomNumericString(6);
        DBContext.Update(otpEntity);
        DBContext.SaveChanges();

        return otpTimeout;
    }

    public void Verify(string phoneNumber, string otp)
    {
        OneTimeCodeEntity otpEntity = DBContext.OneTimeCodes.Where(otp => otp.PhoneNumber.Equals(phoneNumber))
                                                .FirstOrDefault();

        if (otpEntity == null)
        {
            throw new WrongOTPException();
        }

        if (!otpEntity.OneTimeCode.Equals(otp))
        {
            throw new WrongOTPException();
        }

        int otpTimeout = Configuration.GetValue<int>(ConfigurationConstants.OTPTimeout);
        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long createDateUnixTime = otpEntity.CreateDate.ToUnixTimeSeconds();
        if (currentUnixTime < (createDateUnixTime + otpTimeout))
        {
            otpEntity.ValidateDate = DateTimeOffset.UtcNow;
            otpEntity.IsValidated = true;
            DBContext.Update(otpEntity);
            DBContext.SaveChanges();
            return;
        }

        throw new OTPTimeoutException();
    }
}
