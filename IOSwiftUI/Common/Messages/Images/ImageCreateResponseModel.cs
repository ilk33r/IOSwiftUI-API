using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Images;

public class ImageCreateResponseModel : ResponseModel
{
    public string PublicID { get; set; }

    public ImageCreateResponseModel(string publicId) : base()
    {
        PublicID = publicId;
    }
}
