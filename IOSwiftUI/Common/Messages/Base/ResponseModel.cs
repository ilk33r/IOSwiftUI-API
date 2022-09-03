using IOBootstrap.NET.Common.Messages.Base;
using IOBootstrap.NET.Common.Models.Shared;

namespace IOSwiftUI.Common.Messages.Base;

public class ResponseModel : IOResponseModel
{
    public ResponseModel()
    {
    }

    public ResponseModel(IOResponseStatusModel status) : base(status)
    {
    }

    public ResponseModel(int responseStatusMessage) : base(responseStatusMessage)
    {
    }

    
}
