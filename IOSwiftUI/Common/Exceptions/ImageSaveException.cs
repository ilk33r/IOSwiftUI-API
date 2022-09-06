using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class ImageSaveException : IOServiceException
{
    public ImageSaveException() : base(ExceptionMessages.ImageSaveCode, ExceptionMessages.ImageSaveMessage, "")
    {
    }

    public ImageSaveException(string detailedMessage) : base(ExceptionMessages.ImageSaveCode, ExceptionMessages.ImageSaveMessage, detailedMessage)
    {
    }
}
