using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Members;

namespace IOSwiftUI.Common.Messages.Members;

public class GetMemberResponseModel : ResponseModel
{
    public MemberModel Member { get; set; }

    public GetMemberResponseModel(MemberModel member) : base()
    {
        Member = member;
    }
}
