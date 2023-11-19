using IOBootstrap.NET.BackOffice;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IOSwiftUI.BackOffice;

[IOBackoffice]
[EnableCors]
[Produces("application/octet-stream")]
[ApiController]
[Route("[controller]")]
public class GenerateBOPageFilesController : IOGenerateBOPageFilesController<GenerateBOPageFilesViewModel, DatabaseContext>
{
    public GenerateBOPageFilesController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }
}
