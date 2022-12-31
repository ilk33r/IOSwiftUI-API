using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class MemberPairFaceIDRequestModel : RequestModel
{
    [Required]
    [MaxLength(256)]
    public string AuthenticationKey { get; set; }
}
