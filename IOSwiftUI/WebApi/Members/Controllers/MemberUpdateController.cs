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
public class MemberUpdateController : Controller<MemberUpdateViewModel>
{
    public MemberUpdateController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPatch("[action]")]
    public ResponseModel ChangePassword([FromBody] ChangePasswordRequestModel requestModel)
    {
        ViewModel.ChangePassword(requestModel.OldPassword, requestModel.NewPassword);
        return new ResponseModel();
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPatch("[action]")]
    public ResponseModel UpdateMember([FromBody] RegisterMemberRequestModel requestModel)
    {
        ViewModel.UpdateMember(requestModel);
        return new ResponseModel();
    }
}
