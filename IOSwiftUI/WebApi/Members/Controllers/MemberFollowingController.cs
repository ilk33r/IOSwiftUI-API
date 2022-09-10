using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.Members.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Members.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class MemberFollowingController : Controller<MemberFollowingViewModel>
{
    public MemberFollowingController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpGet("[action]")]
    public ResponseModel Follow([FromQuery] int memberId)
    {
        ViewModel.Follow(memberId);
        return new ResponseModel();
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpGet("[action]")]
    public ResponseModel UnFollow([FromQuery] int memberId)
    {
        ViewModel.UnFollow(memberId);
        return new ResponseModel();
    }
}
