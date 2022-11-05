using System.Collections.Generic;
using IOBootstrap.NET.Common.Attributes;
using IOBootstrap.NET.Common.Enumerations;
using IOBootstrap.NET.Common.Exceptions.Common;
using IOBootstrap.NET.Common.Logger;
using IOSwiftUI.Common.Messages.Base;
using IOSwiftUI.Common.Messages.DirectMessages;
using IOSwiftUI.Common.Models.DirectMessages;
using IOSwiftUI.Core.Controllers;
using IOSwiftUI.DataAccess.Context;
using IOSwiftUI.WebApi.DirectMessages.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOSwiftUI.WebApi.DirectMessages.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class DirectMessageController : Controller<DirectMessageViewModel>
{
    public DirectMessageController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<IOLoggerType> logger, DatabaseContext dbContext) : base(configuration, environment, logger, dbContext)
    {
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpGet("[action]")]
    public InboxResponseModel GetInboxes()
    {
        IList<InboxModel> inboxes = ViewModel.GetInboxes();
        return new InboxResponseModel(inboxes);
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public CreateInboxResponseModel CreateInbox([FromBody] CreateInboxRequestModel requestModel)
    {
        InboxModel inbox = ViewModel.CreateConversation(requestModel.ToMemberID);
        return new CreateInboxResponseModel(inbox);
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPut("[action]")]
    public GetMessagesResponseModel SendMessage([FromBody] SendMessageRequestModel requestModel)
    {
        MessageModel message = ViewModel.SendMessage(requestModel.ToMemberID, requestModel.EncryptedMessage);
        
        List<MessageModel> messages = new List<MessageModel>();
        messages.Add(message);

        return new GetMessagesResponseModel(messages, null);
    }

    [IORequireHTTPS]
    [IOUserRole(UserRoles.User)]
    [HttpDelete("[action]")]
    public ResponseModel DeleteInbox([FromQuery] int? inboxID)
    {
        if (inboxID == null || inboxID <= 0)
        {
            throw new IOInvalidRequestException();
        }

        ViewModel.DeleteInbox(inboxID ?? 0);
        return new ResponseModel();
    }

    [IORequireHTTPS]
    [IOValidateRequestModel]
    [IOUserRole(UserRoles.User)]
    [HttpPost("[action]")]
    public GetMessagesResponseModel GetMessages([FromBody] GetMessagesRequestModel requestModel)
    {
        return ViewModel.GetMessages(requestModel.Pagination, requestModel.InboxID);
    }
}
