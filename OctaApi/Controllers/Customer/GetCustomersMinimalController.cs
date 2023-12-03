using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.GetCustomersMinimal;
namespace OctaApi.Controllers.Customer;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GetCustomersMinimalController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetCustomersMinimalController> _logger;
    public GetCustomersMinimalController(IMediator mediator, ILogger<GetCustomersMinimalController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetCustomersMinimalRequest();
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);

        }
        catch (Exception e)
        {
            _logger.LogError(e,"");
            return BadRequest();
        }
    }

}
