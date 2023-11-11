using IOBootstrap.NET.BackOffice;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IOSwiftUI.BackOffice;

[IOBackoffice]
[EnableCors]
[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class GenerateBOPageController : IOGenerateBOPageController<GenerateBOPageViewModel, DatabaseContext>
{
    public GenerateBOPageController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }
}
