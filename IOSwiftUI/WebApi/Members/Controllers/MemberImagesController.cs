using System;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Messages.Images;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.Members.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.Members.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class MemberImagesController : Controller<MemberImagesViewModel>
{
    public MemberImagesController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpPut("[action]")]
    public ImageCreateResponseModel AddMemberImage(IFormFile file)
    {
        string fileName = ViewModel.SaveFile(file);
        ViewModel.AddMemberImage(fileName);

        return new ImageCreateResponseModel(ViewModel.CreatePublicId(fileName));
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpDelete("[action]")]
    public ResponseModel DeleteProfilePicture()
    {
        ViewModel.DeleteProfilePicture();
        return new ResponseModel();
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpPut("[action]")]
    public ImageCreateResponseModel UploadProfilePicture(IFormFile file)
    {
        string fileName = ViewModel.SaveFile(file);
        ViewModel.UpdateMemberProfilePicture(fileName);

        return new ImageCreateResponseModel(ViewModel.CreatePublicId(fileName));
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public MemberImagesResponseModel GetImages([FromBody] PaginationRequestModel requestModel)
    {
        return ViewModel.MemberImages(requestModel.Pagination);
    }
}
