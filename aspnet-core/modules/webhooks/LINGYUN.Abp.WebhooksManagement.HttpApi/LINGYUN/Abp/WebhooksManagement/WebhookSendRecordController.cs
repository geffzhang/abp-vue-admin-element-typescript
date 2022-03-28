﻿using LINGYUN.Abp.WebhooksManagement.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.WebhooksManagement;

[RemoteService(Name = WebhooksManagementRemoteServiceConsts.RemoteServiceName)]
[Area(WebhooksManagementRemoteServiceConsts.ModuleName)]
[Authorize(WebhooksManagementPermissions.WebhooksSendAttempts.Default)]
[Route("api/webhooks/send-attempts")]
public class WebhookSendRecordController : WebhooksManagementControllerBase, IWebhookSendRecordAppService
{
    protected IWebhookSendRecordAppService SendRecordAppService { get; }

    public WebhookSendRecordController(IWebhookSendRecordAppService sendRecordAppService)
    {
        SendRecordAppService = sendRecordAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public Task<WebhookSendRecordDto> GetAsync(Guid id)
    {
        return SendRecordAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<WebhookSendRecordDto>> GetListAsync(WebhookSendRecordGetListInput input)
    {
        return SendRecordAppService.GetListAsync(input);
    }

    [HttpPost]
    [Route("{id}/resend")]
    [Authorize(WebhooksManagementPermissions.WebhooksSendAttempts.Resend)]
    public Task ResendAsync(Guid id)
    {
        return SendRecordAppService.ResendAsync(id);
    }
}
