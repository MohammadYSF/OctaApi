using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaApi.Application.Features.VehicleFeatures.GetAllVehicles;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GetAllVehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetAllVehiclesController> _logger;
    public GetAllVehiclesController(IMediator mediator, ILogger<GetAllVehiclesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetAllVehiclesRequest();
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
