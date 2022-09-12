using System;
using IOBootstrap.NET.Common.Exceptions.Base;
using IOSwiftUI.Common.Constants;

namespace IOSwiftUI.Common.Exceptions;

[Serializable]
public class InboxNotFoundException : IOServiceException
{
    public InboxNotFoundException() : base(ExceptionMessages.InboxNotFoundCode, ExceptionMessages.InboxNotFoundMessage, "")
    {
    }

    public InboxNotFoundException(string detailedMessage) : base(ExceptionMessages.InboxNotFoundCode, ExceptionMessages.InboxNotFoundMessage, detailedMessage)
    {
    }
}