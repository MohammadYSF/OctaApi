using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
namespace OctaApi.Controllers.Customer;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AddCustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddCustomerController> _logger;
    public AddCustomerController(IMediator mediator, ILogger<AddCustomerController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Index([FromBody]AddCustomerRequest request)
    {
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
