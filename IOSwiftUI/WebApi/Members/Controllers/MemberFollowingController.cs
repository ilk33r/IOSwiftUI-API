using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Messages.Members;
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
    public MemberFriendsResponseModel GetFriends()
    {
        return ViewModel.GetFriends();
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public ResponseModel Follow([FromBody] MemberFollowingRequestModel requestModel)
    {
        ViewModel.Follow(requestModel.MemberID);
        return new ResponseModel();
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public ResponseModel UnFollow([FromBody] MemberFollowingRequestModel requestModel)
    {
        ViewModel.UnFollow(requestModel.MemberID);
        return new ResponseModel();
    }
}
