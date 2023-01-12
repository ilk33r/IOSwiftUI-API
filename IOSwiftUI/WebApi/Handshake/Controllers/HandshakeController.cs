using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.WebApi.Handshake.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.Handshake.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Handshake.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class HandshakeDefaultController : IOHandshakeController<HandshakeViewModel, DatabaseContext>
{
    public HandshakeDefaultController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }
}
