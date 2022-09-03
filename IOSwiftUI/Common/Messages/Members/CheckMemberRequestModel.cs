using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class CheckMemberRequestModel : RequestModel
{
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
}
