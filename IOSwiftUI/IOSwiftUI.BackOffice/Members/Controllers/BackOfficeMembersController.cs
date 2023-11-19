using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common;
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
public class BackOfficeMembersController : BackOfficeController<BackOfficeMembersViewModel>
{
    public BackOfficeMembersController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext databaseContext) : base(configuration, environment, logger, databaseContext)
    {
    }

    [IOValidateRequestModel]
    [IOUserRole(UserRoles.Admin)]
    [HttpPost("[action]")]
    public BOMemberListResponseModel GetMembers([FromBody] BOMemberListRequestModel requestModel) 
    {
        return ViewModel.GetMembers(requestModel.Start, requestModel.Count);
    }
}
