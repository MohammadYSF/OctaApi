using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
using OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
using OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoiuce;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeleteSellInvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeleteSellInvoiceController> _logger;

    public DeleteSellInvoiceController(IMediator mediator, ILogger<DeleteSellInvoiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpDelete]
    public async Task<IActionResult> Index(DeleteSellInvoiceRequest request)
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
