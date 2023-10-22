using IOBootstrap.NET.BackOffice.User.ViewModels;
using IOSwiftUI.DataAccess.Context;

namespace IOSwiftUI.BackOffice;

public class BackOfficeUserDefaultViewModel : IOUserViewModel<DatabaseContext>
{
    public BackOfficeUserDefaultViewModel() : base()
    {
    }
}
