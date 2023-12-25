using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById;
namespace OctaApi.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class GetInvoiceByIdController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetInvoiceByIdController> _logger;

    public GetInvoiceByIdController(IMediator mediator, ILogger<GetInvoiceByIdController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetInvoiceByIdRequest request)
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
