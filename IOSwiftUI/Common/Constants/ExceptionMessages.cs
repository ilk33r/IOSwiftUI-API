namespace IOSwiftUI.Common.Constants;

public static class ExceptionMessages
{
    public const int ImageCorruptCode = 704;
    public const int ImageSaveCode = 705;
    public const int ImageNotFoundCode = 706;
    public const int MemberAlreadyFollowingCode = 801;
    public const int MemberNotFollowingCode = 802;
    public const int MemberHasAlreadyProfilePictureCode = 803;
    public const int MemberOTPAlreadyValidatedCode = 804;
    public const int MemberOTPAlreadySentCode = 805;
    public const int InboxNotFoundCode = 902;
    public const int WrongOTPCode = 903;
    public const int OTPTimeoutCode = 904;

    public const string ImageCorruptMessage = "Could not read image file.";
    public const string ImageSaveMessage = "Could not save image file.";
    public const string ImageNotFoundMessage = "Could not find image file.";
    public const string MemberAlreadyFollowingMessage= "You are already following this user.";
    public const string MemberNotFollowingMessage = "You are not following this user.";
    public const string MemberHasAlreadyProfilePictureMessage = "You have a profile picture.";
    public const string MemberOTPAlreadyValidatedMessage = "Your one-time password was recently verified.";
    public const string MemberOTPAlreadySentdMessage = "Your one-time password was sent recently.";
    public const string InboxNotFoundMessage = "Could not found inbox.";
    public const string WrongOTPMessage = "Wrong one-time password.";
    public const string OTPTimeoutMessage = "OTP expired.";
}
