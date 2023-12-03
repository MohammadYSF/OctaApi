using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
 namespace OctaApi.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class AddInvoicePaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AddInvoicePaymentController> _logger;

    public AddInvoicePaymentController(IMediator mediator, ILogger<AddInvoicePaymentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index([FromBody]AddInvoicePaymentRequest request)
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
