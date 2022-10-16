using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Messages.DirectMessages;

public class GetMessagesRequestModel : RequestModel
{
    [Required]
    public PaginationModel Pagination { get; set; }

    [Required]
    public int InboxID { get; set; }
}
