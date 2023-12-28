using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById;
public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdRequest, GetInvoiceByIdResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    public GetInvoiceByIdHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }
    public async Task<GetInvoiceByIdResponse> Handle(GetInvoiceByIdRequest request, CancellationToken cancellationToken)
    {
        var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        var sellInvoiceDescriptionRM = await _sellInvoiceQueryRepository.GetSellInvoiceDescriptionRMBySellInvoiceId(request.InvoiceId);

        var response = new GetInvoiceByIdResponse(sellInvoiceRM, sellInvoiceDescriptionRM);
        return response;
    }
}
