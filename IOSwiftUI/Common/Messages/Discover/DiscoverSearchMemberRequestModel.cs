using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Messages.Discover;

public class DiscoverSearchMemberRequestModel : RequestModel
{
    [Required]
    [StringLength(128)]
    public string UserName { get; set; }

    [Required]
    public PaginationModel Pagination { get; set; }
}
