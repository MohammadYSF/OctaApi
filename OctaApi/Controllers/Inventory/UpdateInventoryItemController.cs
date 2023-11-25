using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.UpdateInventoryItem;
namespace OctaApi.Controllers.Inventory;

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
    public async Task<IActionResult> Index(UpdateInventoryItemRequest request)
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
