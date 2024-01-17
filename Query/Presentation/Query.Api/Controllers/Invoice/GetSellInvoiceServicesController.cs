using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
namespace Query.Presentation.Api.Controllers;
//[Authorize]
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
    public async Task<IActionResult> Index([FromQuery] GetSellInvoicecServicesRequest request)
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
