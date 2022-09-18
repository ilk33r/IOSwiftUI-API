using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class OTPAlreadySentException : IOServiceException
{
    public OTPAlreadySentException() : base(ExceptionMessages.MemberOTPAlreadySentCode, ExceptionMessages.MemberOTPAlreadySentdMessage, "")
    {
    }

    public OTPAlreadySentException(string detailedMessage) : base(ExceptionMessages.MemberOTPAlreadySentCode, ExceptionMessages.MemberOTPAlreadySentdMessage, detailedMessage)
    {
    }
}
