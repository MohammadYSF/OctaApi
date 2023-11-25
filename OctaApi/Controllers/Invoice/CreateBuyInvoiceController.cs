using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBuyInvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateBuyInvoiceController> _logger;

    public CreateBuyInvoiceController(IMediator mediator, ILogger<CreateBuyInvoiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index(CreateBuyInvoiceRequest request)
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
