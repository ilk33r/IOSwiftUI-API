using System;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class AuthenticateResponseModel : ResponseModel
{
    public string Token { get; set; }

    public DateTimeOffset? Expire { get; set; }
}
