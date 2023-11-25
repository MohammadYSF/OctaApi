using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
using OctaApi.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateMiscellaneousSellInvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateMiscellaneousSellInvoiceController> _logger;

    public CreateMiscellaneousSellInvoiceController(IMediator mediator, ILogger<CreateMiscellaneousSellInvoiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index(CreateMiscellaneousSellInvoiceRequest request)
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
