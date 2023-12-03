using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems;

namespace OctaApi.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class UpdateInvoiceServicesAndInventoryItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateInvoiceServicesAndInventoryItemsController> _logger;

    public UpdateInvoiceServicesAndInventoryItemsController(IMediator mediator, ILogger<UpdateInvoiceServicesAndInventoryItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPut]
    public async Task<IActionResult> Index([FromBody]UpdateInvoiceServicesAndInventoryItemsRequest request)
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
