using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.DirectMessages;

public class MessageModel : Model
{
    public int MessageID { get; set; }

    public string Message { get; set; }

    public DateTimeOffset MessageDate { get; set; }
}
