using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
using OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
using OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoiuce;
using OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices;
using OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices;
using OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById;
using OctaApi.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;
using OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GetSellInvoiceServicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetSellInvoiceServicesController> _logger;

    public GetSellInvoiceServicesController(IMediator mediator, ILogger<GetSellInvoiceServicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index(GetSellInvoicecServicesRequest request)
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
