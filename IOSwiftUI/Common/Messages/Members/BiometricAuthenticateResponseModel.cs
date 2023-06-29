using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class BiometricAuthenticateResponseModel : ResponseModel
{
    public string BiometricToken { get; set; }
}
