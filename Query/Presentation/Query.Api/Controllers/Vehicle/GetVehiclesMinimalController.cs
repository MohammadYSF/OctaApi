using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace OctaApi.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetVehiclesMinimalController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetVehiclesMinimalController> _logger;
    public GetVehiclesMinimalController(IMediator mediator, ILogger<GetVehiclesMinimalController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetVehiclesMinimalRequest();
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
