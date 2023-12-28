using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.Inventory.GetServices;
namespace OctaApi.Controllers.Inventory;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GetServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetServicesController> _logger;
    public GetServicesController(IMediator mediator, ILogger<GetServicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetServicesRequest();
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
