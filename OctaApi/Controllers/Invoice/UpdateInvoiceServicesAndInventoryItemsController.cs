using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
using OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
using OctaApi.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

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
    public async Task<IActionResult> Index(UpdateInvoiceServicesAndInventoryItemsRequest request)
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
