using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class MemberImagesRequestModel : RequestModel
{
    public string UserName { get; set; }

    [Required]
    public PaginationModel Pagination { get; set; }
}
