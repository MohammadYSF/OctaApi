using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;

namespace Query.Presentation.Api.Controllers;
//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdController> _logger;

    public GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdController(IMediator mediator, ILogger<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest request)
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
