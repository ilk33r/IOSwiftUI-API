using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Messages.Discover;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.Discover.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Discover.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class DiscoverController : Controller<DiscoverViewModel>
{
    public DiscoverController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [IOValidateRequestModel]
    [HttpPost("[action]")]
    public DiscoverImagesResponseModel Discover([FromBody] PaginationRequestModel requestModel)
    {
        return ViewModel.DiscoverImages(requestModel.Pagination);
    }
}
