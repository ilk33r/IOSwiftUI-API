using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.DirectMessages;

public class InboxModel : Model
{
    public int InboxID { get; set; }

    public string UserName { get; set; }

    public string NameSurname { get; set; }

    public string ProfilePicturePublicID { get; set; }

    public DateTimeOffset UpdateDate { get; set; }

    public int UnreadMessageCount { get; set; }

    public MessageModel LastMessage { get; set; }
}
