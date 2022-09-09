using System.IO;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.Statics.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Statics.Controllers;

[Produces("image/jpeg")]
[ApiController]
[Route("[controller]")]
public class ImageAssetController : Controller<ImageAssetViewModel>
{
    public ImageAssetController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpGet("[action]")]
    [ResponseCache(Duration = 604800, Location = ResponseCacheLocation.Any, NoStore = false)]
    public FileStreamResult Get([FromQuery] string publicId)
    {
        FileStream imageFile = ViewModel.GetImageFile(publicId);
        return File(imageFile, "image/jpeg");
    }
}
