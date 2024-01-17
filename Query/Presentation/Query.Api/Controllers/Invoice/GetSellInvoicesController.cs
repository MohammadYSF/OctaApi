using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
namespace Query.Presentation.Api.Controllers;
//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetSellInvoicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetSellInvoicesController> _logger;

    public GetSellInvoicesController(IMediator mediator, ILogger<GetSellInvoicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetSellInvoicesRequest request)
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
