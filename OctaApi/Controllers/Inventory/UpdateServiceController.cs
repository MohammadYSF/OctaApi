using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem;
using OctaApi.Application.Features.InventoryFeatures.UpdateService;
namespace OctaApi.Controllers.Inventory;

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
    public async Task<IActionResult> Index(UpdateServiceRequest request)
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
