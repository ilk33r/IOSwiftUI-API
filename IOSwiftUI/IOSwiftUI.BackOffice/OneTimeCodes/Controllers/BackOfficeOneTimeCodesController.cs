using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Core;
using IOSwiftUI.DataAccess.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IOSwiftUI.BackOffice;

[IOBackoffice]
[EnableCors]
[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class BackOfficeOneTimeCodesController : BackOfficeController<BackOfficeOneTimeCodesViewModel>
{
    public BackOfficeOneTimeCodesController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public BOOneTimeCodeListResponseModel GetOneTimeCodes([FromBody] BOOneTimeCodeListRequestModel requestModel) 
    {
        return ViewModel.GetOneTimeCodes(requestModel.Start, requestModel.Count);
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public ResponseModel UpdateOneTimeCode([FromBody] BOOneTimeCodeUpdateRequestModel requestModel)
    {
        // Update item
        ViewModel.UpdateOneTimeCode(requestModel);

        // Create and return response
        return new ResponseModel();
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public ResponseModel DeleteOneTimeCode([FromBody] BOOneTimeCodeDeleteRequestModel requestModel)
    {
        // Delete item
        ViewModel.DeleteOneTimeCode(requestModel.ID);

        // Create and return response
        return new ResponseModel();
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public ResponseModel CreateOneTimeCode([FromBody] BOOneTimeCodeAddRequestModel requestModel)
    {
        // Create item
        ViewModel.CreateOneTimeCode(requestModel);

        // Create and return response
        return new ResponseModel();
    }
}
