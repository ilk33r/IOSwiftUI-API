using System;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.Core.ViewModels;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberViewModel : ViewModel
{
    public MemberModel GetCurrentMember()
    {
        MemberModel currentMember = IOSerializableUtilities.Copy(CurrentMember);
        currentMember.IsFollowing = false;
        return currentMember;
    }

    public MemberModel GetMemberByName(string userName)
    {
        MemberModel member = DBContext.Members
                                        .Select(m => new MemberModel()
                                        {
                                            ID = m.ID,
                                            UserName = m.UserName,
                                            BirthDate = m.BirthDate,
                                            Name = m.Name,
                                            Surname = m.Surname,
                                            LocationName = m.LocationName,
                                            LocationLatitude = m.LocationLatitude,
                                            LocationLongitude = m.LocationLongitude,
                                            ProfilePicturePublicId = m.ProfilePictureFileName
                                        })
                                        .Where(m => m.UserName.ToLower().Equals(userName))
                                        .FirstOrDefault();

        if (member == null)
        {
            throw new IOUserNotFoundException();
        }
        else 
        {
            if (!String.IsNullOrEmpty(member.ProfilePicturePublicId))
            {
                member.ProfilePicturePublicId = CreatePublicId(member.ProfilePicturePublicId);
            }

            MemberFollowingModel followingMember = CurrentMember.Followings
                                                                .Where(mf => mf.FollowingMemberID == member.ID)
                                                                .FirstOrDefault();
            if (followingMember == null)
            {
                member.IsFollowing = false;
            }
            else
            {
                member.IsFollowing = true;
            }
        }

        return member;
    }
}
