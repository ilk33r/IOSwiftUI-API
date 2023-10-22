using System;
using IOBootstrap.NET.Core.ViewModels;
using IOSwiftUI.DataAccess.Context;

namespace IOSwiftUI.Core;

public class BackOfficeViewModel : IOBackOfficeViewModel<DatabaseContext>
{
    public BackOfficeViewModel() : base()
    {
    }
}
