using System;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Common.Models.Members;
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
public class MemberController : Controller<MemberViewModel>
{
    public MemberController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpGet("[action]")]
    public GetMemberResponseModel Get([FromQuery] string userName)
    {
        MemberModel member = null;
        if (String.IsNullOrEmpty(userName))
        {
            member = ViewModel.GetCurrentMember();
        }
        else
        {
            member = ViewModel.GetMemberByName(userName);
        }

        return new GetMemberResponseModel(member);
    }
}
