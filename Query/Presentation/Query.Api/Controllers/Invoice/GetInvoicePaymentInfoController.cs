﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace Query.Presentation.Api.Controllers;
//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetInvoicePaymentInfoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetInvoicePaymentInfoController> _logger;

    public GetInvoicePaymentInfoController(IMediator mediator, ILogger<GetInvoicePaymentInfoController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetInvoicePaymentInfoRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "");
            return BadRequest();
        }
    }


}
