using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.DirectMessages;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class GetMessagesResponseModel : ResponseModel
{
    public IList<MessageModel> Messages { get; set; }

    public PaginationModel Pagination { get; set; }

    public GetMessagesResponseModel(IList<MessageModel> messages, PaginationModel pagination) : base() 
    {
        Messages = messages;
        Pagination = pagination;
    }
}
