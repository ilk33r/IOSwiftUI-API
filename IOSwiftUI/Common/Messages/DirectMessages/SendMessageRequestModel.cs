using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class SendMessageRequestModel : RequestModel
{
    [Required]
    public int ToMemberID { get; set; }

    [Required]
    public string EncryptedMessage { get; set; }
}
