using System;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberFollowingViewModel : ViewModel
{
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
