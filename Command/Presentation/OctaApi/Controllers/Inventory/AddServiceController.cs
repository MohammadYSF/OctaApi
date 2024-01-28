using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace Command.Presentation.Api.Controllers.Inventory;

//[Authorize]
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
    public async Task<IActionResult> Index([FromBody] AddServiceRequest request)
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
