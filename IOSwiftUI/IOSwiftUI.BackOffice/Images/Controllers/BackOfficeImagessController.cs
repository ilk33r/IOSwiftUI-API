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
public class BackOfficeImagessController : BackOfficeController<BackOfficeImagessViewModel>
{
    public BackOfficeImagessController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public BOImagesListResponseModel GetImagess([FromBody] BOImagesListRequestModel requestModel) 
    {
        return ViewModel.GetImagess(requestModel.Start, requestModel.Count, requestModel.MemberID);
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public ResponseModel UpdateImages([FromBody] BOImagesUpdateRequestModel requestModel)
    {
        // Update item
        ViewModel.UpdateImages(requestModel);

        // Create and return response
        return new ResponseModel();
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public ResponseModel DeleteImages([FromBody] BOImagesDeleteRequestModel requestModel)
    {
        // Delete item
        ViewModel.DeleteImages(requestModel.ID);

        // Create and return response
        return new ResponseModel();
    }
}
