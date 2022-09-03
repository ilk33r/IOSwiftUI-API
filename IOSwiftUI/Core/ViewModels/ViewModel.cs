using IOBootstrap.NET.Core.ViewModels;
using IOSwiftUI.DataAccess.Context;

namespace IOSwiftUI.Core.ViewModels;

public class ViewModel : IOViewModel
{
    public DatabaseContext DBContext { get; set; }
    
    #region Initialization Methods

    public ViewModel()
    {
    }

	#endregion
}
