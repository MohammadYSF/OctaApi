using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;
public sealed class GetInvoicePaymentInfoHandler : IRequestHandler<GetInvoicePaymentInfoRequest, GetInvoicePaymentInfoResponse>
{
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoicePaymentRM> _sellInvoicePaymentRMCacheService;
    private readonly IDistributedCacheService<VehicleRM> _vehicleRMCacheService;
    private readonly IDistributedCacheService<CustomerRM> _customerRMCacheService;

    public GetInvoicePaymentInfoHandler(IVehicleQueryRepository vehicleQueryRepository, ICustomerQueryRepository customerQueryRepository, ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCacheService, IDistributedCacheService<VehicleRM> vehicleRMCacheService, IDistributedCacheService<CustomerRM> customerRMCacheService, IDistributedCacheService<SellInvoicePaymentRM> sellInvoicePaymentRMCacheService)
    {
        _vehicleQueryRepository = vehicleQueryRepository;
        _customerQueryRepository = customerQueryRepository;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceRMCacheService = sellInvoiceRMCacheService;
        _vehicleRMCacheService = vehicleRMCacheService;
        _customerRMCacheService = customerRMCacheService;
        _sellInvoicePaymentRMCacheService = sellInvoicePaymentRMCacheService;
    }

    public async Task<GetInvoicePaymentInfoResponse> Handle(GetInvoicePaymentInfoRequest request, CancellationToken cancellationToken)
    {
        await _customerQueryRepository.CheckCacheAsync();
        await _vehicleQueryRepository.CheckCacheAsync();
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var sellInvoiceRM = _sellInvoiceRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId).FirstOrDefault();
        var sellInvoicePaymentRMs = _sellInvoicePaymentRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId);
        var vehicleRM = _vehicleRMCacheService.FindBy(a => a.VehicleCode == sellInvoiceRM.VehicleCode).FirstOrDefault();
        var customerRM = _customerRMCacheService.FindBy(a => a.CustomerCode == sellInvoiceRM.CustomerCode).FirstOrDefault();
        //var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        //var sellInvoicePaymentRMs = await _sellInvoiceQueryRepository.GetSellInvoicePaymentRMsBySellInvoiceIdAsync(request.InvoiceId);
        //var vehicleRM = await _vehicleQueryRepository.GetByVehicleCodeAsync(sellInvoiceRM.VehicleCode);
        //var customerRM = await _customerQueryRepository.GetByCustomerCodeAsync(sellInvoiceRM.CustomerCode);
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
