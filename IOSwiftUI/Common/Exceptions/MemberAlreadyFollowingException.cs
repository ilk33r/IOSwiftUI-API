using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class MemberAlreadyFollowingException : IOServiceException
{
    public MemberAlreadyFollowingException() : base(ExceptionMessages.MemberAlreadyFollowingCode, ExceptionMessages.MemberAlreadyFollowingMessage, "")
    {
    }

    public MemberAlreadyFollowingException(string detailedMessage) : base(ExceptionMessages.MemberAlreadyFollowingCode, ExceptionMessages.MemberAlreadyFollowingMessage, detailedMessage)
    {
    }
}
