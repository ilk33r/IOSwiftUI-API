using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Constants;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Utilities;
using IOBootstrap.NET.Core.ViewModels;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.Core.ViewModels;

public class ViewModel : IOViewModel
{
    public DatabaseContext DBContext { get; set; }
    public MemberModel CurrentMember { get; set; }
    
    #region Initialization Methods

    public ViewModel()
    {
    }

	#endregion

    public override int GetUserRole()
    {
        // Check authorization header key exists
        string token = null;
		if (Request.Headers.ContainsKey(IORequestHeaderConstants.AuthorizationToken))
		{
            token = Request.Headers[IORequestHeaderConstants.AuthorizationToken];
        }

        if (String.IsNullOrEmpty(token))
        {
            return (int)UserRoles.AnonmyMouse;
        }

        string[] tokenParts = token.Split("-");
        string userIdString = tokenParts[tokenParts.Length - 1];
        int userId = int.Parse(userIdString);
        List<string> tokenPartsList = new List<string>(tokenParts);
        string userToken = String.Join('-', tokenPartsList.Take(tokenPartsList.Count - 1));

        string cacheKey = String.Format(CacheKeys.UserCacheKey, userIdString);
        IOCacheObject cachedObject = IOCache.GetCachedObject(cacheKey);
        if (cachedObject == null)
        {
            CurrentMember = DBContext.Members
                                        .Select(m => new MemberModel()
                                        {
                                            ID = m.ID,
                                            UserName = m.UserName,
                                            UserToken = m.UserToken,
                                            BirthDate = m.BirthDate,
                                            Email = m.Email,
                                            Name = m.Name,
                                            Surname = m.Surname,
                                            LocationName = m.LocationName,
                                            LocationLatitude = m.LocationLatitude,
                                            LocationLongitude = m.LocationLongitude,
                                            ProfilePicturePublicId = m.ProfilePictureFileName,
                                            PhoneNumber = m.PhoneNumber,
                                            UserStatus = m.UserStatus,
                                            Followings = DBContext.MemberFollowings
                                                                        .Select(mf => new MemberFollowingModel()
                                                                        {
                                                                            MemberID = mf.Member.ID,
                                                                            FollowingMemberID = mf.FollowingMember.ID
                                                                        })
                                                                        .Where(mf => mf.MemberID == m.ID)
                                                                        .ToList()
                                        })
                                        .Where(m => m.ID == userId)
                                        .FirstOrDefault();

            if (CurrentMember == null || CurrentMember.UserStatus != UserStatuses.Active || !(CurrentMember.UserToken.Equals(userToken)))
            {
                return (int)UserRoles.AnonmyMouse;
            }
            else
            {
                if (!String.IsNullOrEmpty(CurrentMember.ProfilePicturePublicId))
                {
                    CurrentMember.ProfilePicturePublicId = CreatePublicId(CurrentMember.ProfilePicturePublicId);
                }
                cachedObject = new IOCacheObject(cacheKey, CurrentMember, 60);
                IOCache.CacheObject(cachedObject);
                return (int)UserRoles.User;
            }
        }

        CurrentMember = (MemberModel)cachedObject.Value;
        if (CurrentMember.UserToken.Equals(userToken))
        {
            return (int)UserRoles.User;
        }

        return (int)UserRoles.AnonmyMouse;
    }

    public string CreatePublicId(string fileName)
    {
		byte[] key = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionKey));
		byte[] iv = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionIV));
        IOAESUtilities aesUtilities = new IOAESUtilities(key, iv);
        return aesUtilities.Encrypt(fileName);
    }

    public string GetFileName(string publicId)
    {
		byte[] key = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionKey));
		byte[] iv = Convert.FromBase64String(Configuration.GetValue<string>(IOConfigurationConstants.EncryptionIV));
        IOAESUtilities aesUtilities = new IOAESUtilities(key, iv);
        return aesUtilities.Decrypt(publicId);
    }

    public void CheckOTPValidated(string phoneNumber)
    {
        OneTimeCodeEntity otpEntity = DBContext.OneTimeCodes.Where(otp => otp.PhoneNumber.Equals(phoneNumber))
                                                .FirstOrDefault();

        if (otpEntity == null)
        {
            throw new WrongOTPException();
        }

        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        int otpValidateTimeout = Configuration.GetValue<int>(ConfigurationConstants.OTPValidateTimeout);
        DateTimeOffset validateDate = otpEntity.ValidateDate ?? DateTimeOffset.UtcNow.AddHours(-1);

        if (otpEntity.IsValidated && currentUnixTime > (validateDate.ToUnixTimeSeconds() + otpValidateTimeout))
        {
            throw new WrongOTPException();
        }
    }
}
