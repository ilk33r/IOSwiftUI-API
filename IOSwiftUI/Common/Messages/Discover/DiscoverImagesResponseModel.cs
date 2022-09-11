using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.Discover;

namespace IOSwiftUI.Common.Messages.Discover;

public class DiscoverImagesResponseModel : ResponseModel
{

    public IList<DiscoverImageModel> Images { get; set; }
    public PaginationModel Pagination { get; set; }

    public DiscoverImagesResponseModel(IList<DiscoverImageModel> images, PaginationModel pagination) : base()
    {
        Images = images;
        Pagination = pagination;
    }
}
