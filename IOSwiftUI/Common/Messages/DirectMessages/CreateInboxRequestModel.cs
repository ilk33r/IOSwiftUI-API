using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class CreateInboxRequestModel : RequestModel
{
    [Required]
    public int ToMemberID { get; set; }
}
