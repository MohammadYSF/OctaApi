using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;

namespace Query.Presentation.Api.Controllers;
//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetInvoiceReportInfoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetInvoiceReportInfoController> _logger;

    public GetInvoiceReportInfoController(IMediator mediator, ILogger<GetInvoiceReportInfoController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetInvoiceReportInfoRequest request)
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
