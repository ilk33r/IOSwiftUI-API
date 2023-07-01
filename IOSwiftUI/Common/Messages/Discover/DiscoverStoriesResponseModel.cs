using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Models.Discover;

namespace IOSwiftUI.Common.Messages.Discover;

public class DiscoverStoriesResponseModel : ResponseModel
{
    public IList<DiscoverStoryModel> Stories { get; set; }

    public DiscoverStoriesResponseModel(IList<DiscoverStoryModel> stories) : base()
    {
        Stories = stories;
    }
}
