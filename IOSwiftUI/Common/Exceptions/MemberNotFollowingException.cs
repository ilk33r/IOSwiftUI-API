using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class MemberNotFollowingException : IOServiceException
{
    public MemberNotFollowingException() : base(ExceptionMessages.MemberNotFollowingCode, ExceptionMessages.MemberNotFollowingMessage, "")
    {
    }

    public MemberNotFollowingException(string detailedMessage) : base(ExceptionMessages.MemberNotFollowingCode, ExceptionMessages.MemberNotFollowingMessage, detailedMessage)
    {
    }
}
