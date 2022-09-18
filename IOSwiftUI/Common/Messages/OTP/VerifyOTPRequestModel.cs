using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Members;

namespace IOSwiftUI.Common.Messages.OTP;

public class VerifyOTPRequestModel : SendOTPRequestModel
{
    [Required]
    [StringLength(6)]
    public string OTP { get; set; }
}
