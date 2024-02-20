using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaShared.DTOs.Request;
namespace Query.Presentation.Api.Controllers;
//[Authorize]
[ApiController]
[Route("[controller]")]
public class GetSellInvoiceInventoryItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetSellInvoiceInventoryItemsController> _logger;

    public GetSellInvoiceInventoryItemsController(IMediator mediator, ILogger<GetSellInvoiceInventoryItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetSellInvoiceInventoryItemsRequest request)
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
