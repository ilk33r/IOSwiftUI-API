using System.Collections.Generic;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Discover;

public class DiscoverStoryModel : Model
{
    public IList<DiscoverImageModel> Images { get; set; }
}
