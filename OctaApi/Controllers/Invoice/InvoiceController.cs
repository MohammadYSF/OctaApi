using MediatR;
using Microsoft.AspNetCore.Mvc;
using OctaApi.Controllers.Customer;

namespace OctaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(IMediator mediator, ILogger<InvoiceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
         

}
