﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
namespace OctaApi.Controllers;
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
