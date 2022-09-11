using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class MemberFollowingRequestModel : RequestModel
{
    [Required]
    public int MemberID { get; set; }
}
