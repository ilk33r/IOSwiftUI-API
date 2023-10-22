using IOBootstrap.NET.BackOffice.Authentication.ViewModels;
using IOSwiftUI.DataAccess.Context;

namespace IOSwiftUI.BackOffice;

public class BackOfficeAuthenticationDefaultViewModel : IOAuthenticationViewModel<DatabaseContext>
{
    public BackOfficeAuthenticationDefaultViewModel() : base()
    {
    }
}
