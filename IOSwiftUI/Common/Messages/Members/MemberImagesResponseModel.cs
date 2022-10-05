using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.Members;

namespace IOSwiftUI.Common.Messages.Members;

public class MemberImagesResponseModel : ResponseModel
{
    public IList<MemberImageModel> Images { get; set; }
    public PaginationModel Pagination { get; set; }

    public MemberImagesResponseModel(IList<MemberImageModel> images, PaginationModel pagination) : base()
    {
        Images = images;
        Pagination = pagination;
    }
}
