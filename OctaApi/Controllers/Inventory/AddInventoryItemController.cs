using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
namespace OctaApi.Controllers.Inventory;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AddInventoryItemController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddInventoryItemController> _logger;
    public AddInventoryItemController(IMediator mediator, ILogger<AddInventoryItemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index([FromBody]AddInventoryItemRequest request)
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
