using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class ImageNotFoundException : IOServiceException
{
    public ImageNotFoundException() : base(ExceptionMessages.ImageNotFoundCode, ExceptionMessages.ImageNotFoundMessage, "")
    {
    }

    public ImageNotFoundException(string detailedMessage) : base(ExceptionMessages.ImageNotFoundCode, ExceptionMessages.ImageNotFoundMessage, detailedMessage)
    {
    }
}
