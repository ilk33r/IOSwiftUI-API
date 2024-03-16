using System;
using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOImagesListResponseModel : ResponseModel
{
    public int Count { get; set; }
    public IList<BOImagesModel> ImagesList { get; set; }

    public BOImagesListResponseModel(int count, IList<BOImagesModel> imagesList) : base()
    {
        Count = count;
        ImagesList = imagesList;
    }
}
