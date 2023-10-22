using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.BackOffice.ViewModels;
using IOSwiftUI.Core;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IOSwiftUI.BackOffice;

[IOBackoffice]
[EnableCors]
[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class BackOfficeDefaultController : BackOfficeController<BackOfficeDefaultViewModel>
{
    public BackOfficeDefaultController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }
}
