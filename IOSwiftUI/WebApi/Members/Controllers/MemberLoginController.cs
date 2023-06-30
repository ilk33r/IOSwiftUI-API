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
public class MemberLoginController : Controller<MemberLoginViewModel>
{
    public MemberLoginController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public AuthenticateResponseModel Authenticate([FromBody] AuthenticateRequestModel requestModel)
    {
        if (!IORegexUtility.IsValidEmail(requestModel.Email))
        {
            throw new IOInvalidRequestException();
        }
        
        return ViewModel.Authenticate(requestModel);
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public BiometricAuthenticateResponseModel BiometricToken([FromBody] BiometricAuthenticateRequestModel requestModel)
    {
        string biometricToken = ViewModel.BiometricToken(requestModel.UserName);
        return new BiometricAuthenticateResponseModel()
        {
            BiometricToken = biometricToken
        };
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public AuthenticateResponseModel AuthenticateWithBiometric([FromBody] AuthenticateWithBiometricRequestModel requestModel)
    {
        return ViewModel.AuthenticateWithBiometric(requestModel);
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpGet("[action]")]
    public ResponseModel CheckToken()
    {
        return new ResponseModel();
    }
}
