using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.Core.Controllers;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.Core.Controllers;

public class Controller<TViewModel> : IOController<TViewModel> where TViewModel : ViewModel, new()
{

    public DatabaseContext DBContext { get; set; }

    public Controller(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger)
    {
        DBContext = dbContext;
        ViewModel.DBContext = DBContext;
    }
}
