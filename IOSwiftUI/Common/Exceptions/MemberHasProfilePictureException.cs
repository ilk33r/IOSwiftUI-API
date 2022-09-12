using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class MemberHasProfilePictureException : IOServiceException
{
    public MemberHasProfilePictureException() : base(ExceptionMessages.MemberHasAlreadyProfilePictureCode, ExceptionMessages.MemberHasAlreadyProfilePictureMessage, "")
    {
    }

    public MemberHasProfilePictureException(string detailedMessage) : base(ExceptionMessages.MemberHasAlreadyProfilePictureCode, ExceptionMessages.MemberHasAlreadyProfilePictureMessage, detailedMessage)
    {
    }
}
