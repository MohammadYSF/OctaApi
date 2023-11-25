﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.AddService;
namespace OctaApi.Controllers.Inventory;

[ApiController]
[Route("[controller]")]
public class AddServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddServiceController> _logger;
    public AddServiceController(IMediator mediator, ILogger<AddServiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index(AddServiceRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);

        }
        catch (Exception e)
        {
            _logger.LogError(e,"");
            return BadRequest();
        }
    }

}
