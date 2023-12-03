using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
namespace OctaApi.Controllers.Inventory;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DeleteInventoryItemController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<DeleteInventoryItemController> _logger;
    public DeleteInventoryItemController(IMediator mediator, ILogger<DeleteInventoryItemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpDelete]
    public async Task<IActionResult> Index([FromQuery]DeleteInventoryItemRequest request)
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
