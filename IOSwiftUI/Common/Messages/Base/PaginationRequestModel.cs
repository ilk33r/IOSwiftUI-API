using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Messages.Base;

public class PaginationRequestModel : RequestModel
{
    [Required]
    public PaginationModel Pagination { get; set; }
}
