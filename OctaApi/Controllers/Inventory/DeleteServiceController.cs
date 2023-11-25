using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.DeleteService;
namespace OctaApi.Controllers.Inventory;

[ApiController]
[Route("[controller]")]
public class DeleteServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<DeleteServiceController> _logger;
    public DeleteServiceController(IMediator mediator, ILogger<DeleteServiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpDelete]
    public async Task<IActionResult> Index(DeleteServiceRequest request)
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
