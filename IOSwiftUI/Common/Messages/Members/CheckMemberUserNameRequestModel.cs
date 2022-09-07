using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class CheckMemberUserNameRequestModel : RequestModel
{
    [Required]
    [StringLength(128)]
    public string UserName { get; set; }
}
