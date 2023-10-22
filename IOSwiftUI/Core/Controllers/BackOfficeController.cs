using System;
using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.Core;

public class BackOfficeController<TViewModel> : IOBackOfficeController<TViewModel, DatabaseContext>
where TViewModel : BackOfficeViewModel, new()
{
    public BackOfficeController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }
}
