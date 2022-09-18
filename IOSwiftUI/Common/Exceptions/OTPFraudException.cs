using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class OTPFraudException : IOServiceException
{
    public OTPFraudException() : base(ExceptionMessages.MemberOTPAlreadyValidatedCode, ExceptionMessages.MemberOTPAlreadyValidatedMessage, "")
    {
    }

    public OTPFraudException(string detailedMessage) : base(ExceptionMessages.MemberOTPAlreadyValidatedCode, ExceptionMessages.MemberOTPAlreadyValidatedMessage, detailedMessage)
    {
    }
}
