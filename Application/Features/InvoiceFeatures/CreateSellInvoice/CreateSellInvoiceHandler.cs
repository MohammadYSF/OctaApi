using Application.Repositories;
using Domain.SellInvoice;
using MediatR;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
internal class CreateSellInvoiceHandler : IRequestHandler<CreateSellInvoiceRequest, CreateSellInvoiceResponse>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
    public CreateSellInvoiceHandler(IInvoiceRepository invoiceRepository, ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
        _sellInvoiceRepository = sellInvoiceRepository;
    }

    public async Task<CreateSellInvoiceResponse> Handle(CreateSellInvoiceRequest request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;
        int code = await _sellInvoiceRepository.GenerateNewCodeAsync();
        var aggregate = SellInvoiceAggregate.Create(id, createDate, code, request.CustomerId, request.VehicleId);
        await _sellInvoiceRepository.AddAsync(aggregate);
        //Invoice invoice = new();
        //invoice.Id = Guid.NewGuid();
        //invoice.VehicleId = request.VehicleId;
        //invoice.RegisterDate = DateTime.Now;
        //invoice.Code = await _invoiceRepository.GetNewInvoiceCode();
        //invoice.Type = Domain.Enums.InvoiceType.Sell;
        //await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.SaveAsync(cancellationToken);
        //var response = new CreateSellInvoiceResponse(invoice.Id, invoice.Code);
        var response = new CreateSellInvoiceResponse();
        return response;
    }
}
