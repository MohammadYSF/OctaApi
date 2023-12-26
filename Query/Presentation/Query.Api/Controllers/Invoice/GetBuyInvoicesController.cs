using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Application.Features.InvoiceFeatures.GetBuyInvoices;
namespace Query.Presentation.Api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class GetBuyInvoicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetBuyInvoicesController> _logger;

    public GetBuyInvoicesController(IMediator mediator, ILogger<GetBuyInvoicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]GetBuyInvoicesRequest request)
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
