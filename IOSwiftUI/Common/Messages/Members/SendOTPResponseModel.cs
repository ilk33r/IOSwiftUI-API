using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class SendOTPResponseModel : ResponseModel
{
    public int OTPTimeout { get; set; }

    public SendOTPResponseModel(int timeout): base()
    {
        OTPTimeout = timeout;
    }
}
