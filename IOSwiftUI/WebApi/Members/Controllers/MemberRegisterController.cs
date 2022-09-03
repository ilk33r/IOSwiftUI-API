using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Exceptions.Common;
using IOBootstrap.NET.Common.Logger;
using IOBootstrap.NET.Common.Utilities;
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
public class MemberRegisterController : Controller<MemberRegisterViewModel>
{
    public MemberRegisterController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public ResponseModel CheckMember([FromBody] CheckMemberRequestModel requestModel)
    {
        if (!IORegexUtility.IsValidEmail(requestModel.Email))
        {
            throw new IOInvalidRequestException();
        }
        
        ViewModel.CheckMember(requestModel.Email);
        return new ResponseModel();
    }
}
