using System;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Exceptions.Common;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberLoginViewModel : ViewModel
{
    public AuthenticateResponseModel Authenticate(AuthenticateRequestModel requestModel)
    {
        string descryptedPassword;
        if (Configuration.GetValue<bool>(ConfigurationConstants.PasswordDecryptionEnabledKey))
        {
            descryptedPassword = DecryptString(requestModel.Password);
        }
        else
        {
            descryptedPassword = requestModel.Password;
        }
        MemberEntity member = DatabaseContext.Members.Where(m => m.Email.ToLower().Equals(requestModel.Email.ToLower()))
                                        .FirstOrDefault();
        if (member == null)
        {
            throw new IOUserNotFoundException();
        }

        if (member.UserStatus != UserStatuses.Active)
        {
            throw new IOInvalidPermissionException();
        }

        if (!IOPasswordUtilities.VerifyPassword(descryptedPassword, member.Password))
        {
            throw new IOInvalidCredentialsException();
        }

        string userToken = IORandomUtilities.GenerateGUIDString();

        member.UserToken = userToken;
        member.TokenDate = DateTime.UtcNow;
        DatabaseContext.Update(member);
        DatabaseContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, member.ID.ToString());
        IOCache.InvalidateCache(cacheKey);

        string tokenWithUserID = String.Format("{0}-{1}", userToken, member.ID);
        return new AuthenticateResponseModel()
        {
            Token = tokenWithUserID,
            Expire = null
        };
    }

    public string BiometricToken(string userName)
    {
        string decryptedUserName = DecryptString(userName);
        
        MemberFaceIDModel faceIDModel = DatabaseContext.MemberFaceIDs
                                                        .Select(m => new MemberFaceIDModel() {
                                                            ID = m.ID,
                                                            MemberID = m.Member.ID,
                                                            UserName = m.Member.UserName,
                                                            AuthenticationKey = m.AuthenticationKey,
                                                        })
                                                        .Where(m => m.UserName.ToLower().Equals(decryptedUserName.ToLower()))
                                                        .FirstOrDefault();

        if (faceIDModel == null)
        {
            throw new IOUserNotFoundException();
        }
        
        faceIDModel.BiometricToken = IORandomUtilities.GenerateRandomAlphaNumericString(12);
        
        string cacheKey = String.Format(CacheKeys.BiometricLoginCacheKey, decryptedUserName);
        IOCacheObject cacheObject = new IOCacheObject(cacheKey, faceIDModel, 60);
        IOCache.CacheObject(cacheObject);

        return faceIDModel.BiometricToken;
    }
}
