using System;
using IOBootstrap.NET.Common.Constants;
using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.Common.Models.Shared;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.WebApi.Home.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Home.Controllers;

public class HomeController : Controller<HomeViewModel>
{
    public HomeController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger) : base(configuration, environment, logger)
    {
    }

    public ResponseModel Index()
    {
        // Obtain app version
        string appVersion = Configuration.GetValue<string>(IOConfigurationConstants.Version);

        // Create response status model
        IOResponseStatusModel responseStatus = new IOResponseStatusModel(IOResponseStatusMessages.OK,
                                                                        "IO Swift UI API",
                                                                        true,
                                                                        String.Format("Version: {0}", appVersion));

        // Return response
        return new ResponseModel(responseStatus);
    }
}
