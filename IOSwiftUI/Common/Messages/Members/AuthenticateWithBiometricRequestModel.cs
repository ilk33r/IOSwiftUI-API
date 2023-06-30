using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class AuthenticateWithBiometricRequestModel : RequestModel
{
    [Required]
    [StringLength(128)]
    public string UserName { get; set; }

    [Required]
    public string BiometricPassword { get; set; }
}
