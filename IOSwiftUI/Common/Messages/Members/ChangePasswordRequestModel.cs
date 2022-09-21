using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class ChangePasswordRequestModel : RequestModel
{
    [Required]
    [StringLength(512, MinimumLength = 8)]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(512, MinimumLength = 8)]
    public string NewPassword { get; set; }
}
