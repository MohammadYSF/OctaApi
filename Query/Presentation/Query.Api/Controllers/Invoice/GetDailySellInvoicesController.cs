using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices;
namespace OctaApi.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class GetDailySellInvoicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetDailySellInvoicesController> _logger;

    public GetDailySellInvoicesController(IMediator mediator, ILogger<GetDailySellInvoicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            GetDailySellInvoicesRequest request = new();
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
