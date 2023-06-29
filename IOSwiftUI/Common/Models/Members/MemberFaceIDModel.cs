using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Members;

public class MemberFaceIDModel : Model
{
    public int ID { get; set; }

    public int MemberID { get; set; }

    public string UserName { get; set; }

    public string AuthenticationKey { get; set; }

    public string BiometricToken { get; set; }
}
