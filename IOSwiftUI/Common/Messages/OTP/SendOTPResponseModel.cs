using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.OTP;

public class SendOTPResponseModel : ResponseModel
{
    public int OTPTimeout { get; set; }

    public SendOTPResponseModel(int timeout): base()
    {
        OTPTimeout = timeout;
    }
}
