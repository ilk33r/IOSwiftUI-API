using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class ImageCorruptException : IOServiceException
{
    public ImageCorruptException() : base(ExceptionMessages.ImageCorruptCode, ExceptionMessages.ImageCorruptMessage, "")
    {
    }

    public ImageCorruptException(string detailedMessage) : base(ExceptionMessages.ImageCorruptCode, ExceptionMessages.ImageCorruptMessage, detailedMessage)
    {
    }
}
