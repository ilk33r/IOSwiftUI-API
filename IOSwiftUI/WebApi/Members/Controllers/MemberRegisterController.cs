using System;
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

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public ResponseModel CheckMemberUserName([FromBody] CheckMemberUserNameRequestModel requestModel)
    {
        if (IORegexUtility.HasSpecialCharacter(requestModel.UserName))
        {
            throw new IOInvalidRequestException();
        }

        ViewModel.CheckMemberUserName(requestModel.UserName);
        return new ResponseModel();
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public ResponseModel PairFaceID([FromBody] MemberPairFaceIDRequestModel requestModel)
    {
        ViewModel.PairFaceID(requestModel.AuthenticationKey);
        return new ResponseModel();
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPut("[action]")]
    public ResponseModel Register([FromBody] RegisterMemberRequestModel requestModel)
    {
        if (!IORegexUtility.IsValidEmail(requestModel.Email))
        {
            throw new IOInvalidRequestException();
        }

        if (IORegexUtility.HasSpecialCharacter(requestModel.UserName))
        {
            throw new IOInvalidRequestException();
        }
        
        ViewModel.RegisterMember(requestModel);
        return new ResponseModel();
    }
}
