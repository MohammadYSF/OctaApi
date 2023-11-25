﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
namespace OctaApi.Controllers.Customer;

[ApiController]
[Route("[controller]")]
public class GetInventoryItems : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetInventoryItems> _logger;
    public GetInventoryItems(IMediator mediator, ILogger<GetInventoryItems> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetCustomersRequest();
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
