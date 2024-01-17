using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace Command.Presentation.Api.Controllers.Inventory;
[Authorize]
[ApiController]
[Route("[controller]")]
public class UpdateInventoryItemController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<UpdateInventoryItemController> _logger;
    public UpdateInventoryItemController(IMediator mediator, ILogger<UpdateInventoryItemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPut]
    public async Task<IActionResult> Index([FromBody] UpdateInventoryItemRequest request)
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
