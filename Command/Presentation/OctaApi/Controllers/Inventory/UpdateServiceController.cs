using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace Command.Presentation.Api.Controllers.Inventory;
[Authorize]
[ApiController]
[Route("[controller]")]
public class UpdateServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<UpdateServiceController> _logger;
    public UpdateServiceController(IMediator mediator, ILogger<UpdateServiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPut]
    public async Task<IActionResult> Index([FromBody] UpdateServiceRequest request)
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
