using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class WrongOTPException : IOServiceException
{
    public WrongOTPException() : base(ExceptionMessages.WrongOTPCode, ExceptionMessages.WrongOTPMessage, "")
    {
    }

    public WrongOTPException(string detailedMessage) : base(ExceptionMessages.WrongOTPCode, ExceptionMessages.WrongOTPMessage, detailedMessage)
    {
    }
}
