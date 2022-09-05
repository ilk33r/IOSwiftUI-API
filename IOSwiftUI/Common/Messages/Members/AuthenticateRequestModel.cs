using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class AuthenticateRequestModel : RequestModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
