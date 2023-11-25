using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Application.Features.CustomerFeatures.AddCustomer;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
namespace OctaApi.Controllers.Customer;

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
    public async Task<IActionResult> Index(AddCustomerRequest request)
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
