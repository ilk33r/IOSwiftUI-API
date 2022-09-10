using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Members;

public class MemberFollowingModel : Model
{
    public int MemberID { get; set; }

    public int FollowingMemberID { get; set; }
}
