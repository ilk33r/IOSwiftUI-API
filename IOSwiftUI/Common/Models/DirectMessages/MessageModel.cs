using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.DirectMessages;

public class MessageModel : Model
{
    public int? InboxID { get; set; }

    public int MessageID { get; set; }

    public string Message { get; set; }

    public DateTimeOffset MessageDate { get; set; }

    public bool? IsSent { get; set; }

    public string UserAvatarPublicID { get; set; }
}
