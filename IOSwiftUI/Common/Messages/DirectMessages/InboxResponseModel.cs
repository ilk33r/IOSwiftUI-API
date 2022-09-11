using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.DirectMessages;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class InboxResponseModel : ResponseModel
{
    public IList<InboxModel> Inboxes { get; set; }

    public InboxResponseModel(IList<InboxModel> inboxes) : base()
    {
        Inboxes = inboxes;
    }
}
