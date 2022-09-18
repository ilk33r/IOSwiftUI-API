using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class OTPTimeoutException : IOServiceException
{
    public OTPTimeoutException() : base(ExceptionMessages.OTPTimeoutCode, ExceptionMessages.OTPTimeoutMessage, "")
    {
    }

    public OTPTimeoutException(string detailedMessage) : base(ExceptionMessages.OTPTimeoutCode, ExceptionMessages.OTPTimeoutMessage, detailedMessage)
    {
    }
}
