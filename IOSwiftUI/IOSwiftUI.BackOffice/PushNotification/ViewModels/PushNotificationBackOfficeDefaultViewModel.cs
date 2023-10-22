using IOBootstrap.NET.BackOffice.PushNotification.ViewModels;
using IOSwiftUI.DataAccess.Context;

namespace IOSwiftUI.BackOffice;

public class PushNotificationBackOfficeDefaultViewModel : IOPushNotificationBackOfficeViewModel<DatabaseContext>
{
    public PushNotificationBackOfficeDefaultViewModel() : base()
    {
    }
}
