using MediatR;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;
public sealed class GetInvoicePaymentInfoHandler : IRequestHandler<GetInvoicePaymentInfoRequest, GetInvoicePaymentInfoResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    public GetInvoicePaymentInfoHandler(IVehicleQueryRepository vehicleQueryRepository, ICustomerQueryRepository customerQueryRepository, ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
        _customerQueryRepository = customerQueryRepository;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }

    public async Task<GetInvoicePaymentInfoResponse> Handle(GetInvoicePaymentInfoRequest request, CancellationToken cancellationToken)
    {
        var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        var sellInvoicePaymentRMs = await _sellInvoiceQueryRepository.GetSellInvoicePaymentRMsBySellInvoiceIdAsync(request.InvoiceId);
        var vehicleRM = await _vehicleQueryRepository.GetByVehicleCodeAsync(sellInvoiceRM.VehicleCode);
        var customerRM = await _customerQueryRepository.GetByCustomerCodeAsync(sellInvoiceRM.CustomerCode);
        var response = new GetInvoicePaymentInfoResponse()
        {
            CustomerRM = customerRM,
            SellInvoicePaymentRMs = sellInvoicePaymentRMs,
            SellInvoiceRM = sellInvoiceRM,
            VehicleRM = vehicleRM
        };
        return response;
    }
}
