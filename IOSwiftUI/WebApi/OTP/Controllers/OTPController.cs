using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Messages.OTP;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.OTP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.OTP.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class OTPController : Controller<OTPViewModel>
{
    public OTPController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public SendOTPResponseModel Send([FromBody] SendOTPRequestModel requestModel)
    {
        return new SendOTPResponseModel(ViewModel.Send(requestModel.PhoneNumber));
    }

    [IOValidateRequestModel]
    [IORequireHTTPS]
    [IOUserRole(UserRoles.AnonmyMouse)]
    [HttpPost("[action]")]
    public ResponseModel Verify([FromBody] VerifyOTPRequestModel requestModel)
    {
        ViewModel.Verify(requestModel.PhoneNumber, requestModel.OTP);
        return new ResponseModel();
    }
}
