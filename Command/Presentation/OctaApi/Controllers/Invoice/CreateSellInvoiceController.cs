﻿using Command.Core.Application.Features.InvoiceFeatures.CreateInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Command.Presentation.Api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class CreateSellInvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateSellInvoiceController> _logger;

    public CreateSellInvoiceController(IMediator mediator, ILogger<CreateSellInvoiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] CreateSellInvoiceRequest request)
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
