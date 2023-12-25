using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
namespace OctaApi.Controllers.Customer;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GetCustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<GetCustomersController> _logger;
    public GetCustomersController(IMediator mediator, ILogger<GetCustomersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = new GetCustomersRequest();
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
