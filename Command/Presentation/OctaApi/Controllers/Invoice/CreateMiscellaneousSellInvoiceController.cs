using Command.Core.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Command.Presentation.Api.Controllers;
[Authorize]
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
    public async Task<IActionResult> Index([FromBody] CreateMiscellaneousSellInvoiceRequest request)
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
