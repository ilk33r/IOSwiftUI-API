using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.Core.Controllers;
using IOSwiftUI.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.Core.Controllers;

public class Controller<TViewModel> : IOController<TViewModel> where TViewModel : ViewModel, new()
{
    public Controller(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger) : base(configuration, environment, logger)
    {
    }
}
