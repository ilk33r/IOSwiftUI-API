using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.DirectMessages;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class CreateInboxResponseModel : ResponseModel
{
    public InboxModel Inbox { get; set; }

    public CreateInboxResponseModel(InboxModel inbox) : base()
    {
        Inbox = inbox;
    }
}
