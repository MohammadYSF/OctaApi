using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice;
namespace OctaApi.Controllers;
[Authorize]
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
    public async Task<IActionResult> Index([FromBody]CreateBuyInvoiceRequest request)
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
