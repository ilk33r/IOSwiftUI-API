using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberFollowingViewModel : ViewModel
{

    public MemberFriendsResponseModel GetFriends()
    {
        int[] followingIds = CurrentMember.Followings
                                            .Select(f => f.FollowingMemberID)
                                            .ToArray();

        List<MemberFriendModel> followings = DBContext.MemberFollowings
                                                .Select(mf => new MemberFriendModel()
                                                {
                                                    ID = mf.FollowingMember.ID,
                                                    OtherMemberID = CurrentMember.ID,
                                                    UserName = mf.FollowingMember.UserName,
                                                    FollowDate = mf.FollowDate,
                                                    UserNameAndSurname = mf.FollowingMember.Name + " " + mf.FollowingMember.Surname,
                                                    LocationName = mf.FollowingMember.LocationName,
                                                    LocationLatitude = mf.FollowingMember.LocationLatitude,
                                                    LocationLongitude = mf.FollowingMember.LocationLongitude,
                                                    ProfilePicturePublicId = mf.FollowingMember.ProfilePictureFileName
                                                })
                                                .Where(mf => followingIds.Contains(mf.ID))
                                                .OrderByDescending(mf => mf.FollowDate)
                                                .ToList();

        List<MemberFriendModel> followers = DBContext.MemberFollowings
                                                .Select(mf => new MemberFriendModel()
                                                {
                                                    ID = mf.Member.ID,
                                                    OtherMemberID = mf.FollowingMember.ID,
                                                    UserName = mf.Member.UserName,
                                                    FollowDate = mf.FollowDate,
                                                    UserNameAndSurname = mf.Member.Name + " " + mf.Member.Surname,
                                                    LocationName = mf.Member.LocationName,
                                                    LocationLatitude = mf.Member.LocationLatitude,
                                                    LocationLongitude = mf.Member.LocationLongitude,
                                                    ProfilePicturePublicId = mf.Member.ProfilePictureFileName
                                                })
                                                .Where(mf => mf.OtherMemberID == CurrentMember.ID)
                                                .OrderByDescending(mf => mf.FollowDate)
                                                .ToList();

        foreach (MemberFriendModel item in followings)
        {
            if (!String.IsNullOrEmpty(item.ProfilePicturePublicId))
            {
                item.ProfilePicturePublicId = CreatePublicId(item.ProfilePicturePublicId);
            }
        }

        foreach (MemberFriendModel item in followers)
        {
            if (!String.IsNullOrEmpty(item.ProfilePicturePublicId))
            {
                item.ProfilePicturePublicId = CreatePublicId(item.ProfilePicturePublicId);
            }
        }

        return new MemberFriendsResponseModel() {
            Followers = followers,
            Followings = followings
        };
    }

    public void Follow(int memberId)
    {
        MemberFollowingModel followingMember = CurrentMember.Followings
                                                                .Where(mf => mf.FollowingMemberID == memberId)
                                                                .FirstOrDefault();

        if (followingMember != null)
        {
            throw new MemberAlreadyFollowingException();
        }

        if (CurrentMember.ID == memberId)
        {
            throw new MemberAlreadyFollowingException();
        }

        MemberEntity memberEntity = new MemberEntity()
        {
            ID = CurrentMember.ID
        };

        MemberEntity followingMemberEntity = new MemberEntity()
        {
            ID = memberId
        };

        DBContext.Attach(memberEntity);
        DBContext.Attach(followingMemberEntity);
        
        MemberFollowingEntity following = new MemberFollowingEntity()
        {
            FollowDate = DateTime.UtcNow,
            Member = memberEntity,
            FollowingMember = followingMemberEntity
        };

        DBContext.Add(following);
        DBContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, CurrentMember.ID);
        IOCache.InvalidateCache(cacheKey);
    }

    public void UnFollow(int memberId)
    {
        MemberFollowingModel followingMember = CurrentMember.Followings
                                                                .Where(mf => mf.FollowingMemberID == memberId)
                                                                .FirstOrDefault();

        if (followingMember == null)
        {
            throw new MemberNotFollowingException();
        }

        MemberFollowingEntity followingMemberEntity = DBContext.MemberFollowings
                                                                .Where(mf => mf.Member.ID == CurrentMember.ID && mf.FollowingMember.ID == memberId)
                                                                .FirstOrDefault();

        if (followingMemberEntity == null)
        {
            throw new IOUserNotFoundException();
        }

        DBContext.Remove(followingMemberEntity);
        DBContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, CurrentMember.ID);
        IOCache.InvalidateCache(cacheKey);
    }
}
