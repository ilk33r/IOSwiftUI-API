using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.OTP;

public class SendOTPRequestModel : RequestModel
{
    [Required]
    [StringLength(16)]
    public string PhoneNumber { get; set; }
}
