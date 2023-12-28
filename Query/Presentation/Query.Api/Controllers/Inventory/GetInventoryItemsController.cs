using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.Inventory.GetInventoryItems;
namespace OctaApi.Controllers.Inventory;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GetInventoryItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetInventoryItemsController> _logger;
    public GetInventoryItemsController(IMediator mediator, ILogger<GetInventoryItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetInventoryItemsRequest();
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
