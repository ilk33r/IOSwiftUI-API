using System;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Models;
using IOSwiftUI.Core.ViewModels;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberViewModel : ViewModel
{
    public MemberModel GetCurrentMember()
    {
        MemberModel currentMember = IOSerializableUtilities.Copy(CurrentMember);
        currentMember.UserStatus = null;
        currentMember.UserToken = null;
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
            member.ProfilePicturePublicId = CreatePublicId(member.ProfilePicturePublicId);
        }

        return member;
    }
}
