using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Constants;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Core.ViewModels;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Models;
using IOSwiftUI.DataAccess.Context;

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

        IOCacheObject cachedObject = IOCache.GetCachedObject(CacheKeys.UserCacheKey);
        if (cachedObject == null)
        {
            string[] tokenParts = token.Split("-");
            string userIdString = tokenParts[tokenParts.Length - 1];
            int userId = int.Parse(userIdString);
            List<string> tokenPartsList = new List<string>(tokenParts);
            string userToken = String.Join('-', tokenPartsList.Take(tokenPartsList.Count - 1));

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
                                            ProfilePictureFileName = m.ProfilePictureFileName,
                                            UserStatus = m.UserStatus
                                        })
                                        .Where(m => m.ID == userId)
                                        .FirstOrDefault();

            if (CurrentMember == null || CurrentMember.UserStatus != UserStatuses.Active)
            {
                return (int)UserRoles.AnonmyMouse;
            }
            else
            {
                cachedObject = new IOCacheObject(CacheKeys.UserCacheKey, CurrentMember, 60);
                IOCache.CacheObject(cachedObject);
                return (int)UserRoles.User;
            }
        }

        CurrentMember = (MemberModel)cachedObject.Value;
        return (int)UserRoles.User;
    }
}
