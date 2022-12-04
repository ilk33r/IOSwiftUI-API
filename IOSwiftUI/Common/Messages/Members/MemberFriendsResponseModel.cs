using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Members;

namespace IOSwiftUI.Common.Messages.Members;

public class MemberFriendsResponseModel : ResponseModel
{
    public IList<MemberFriendModel> Followers { get; set; }

    public IList<MemberFriendModel> Followings { get; set; }
}
