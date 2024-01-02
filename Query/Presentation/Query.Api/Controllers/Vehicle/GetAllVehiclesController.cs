using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Application.Features.VehicleFeatures.GetAllVehicles;
namespace OctaApi.Controllers;
//[Authorize]
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
