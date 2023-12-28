using MediatR;

namespace Command.Core.Application.Features.InventoryFeatures.UpdateService
{
    public sealed record UpdateServiceRequest(Guid Id, string Name, long DefaultPrice) : IRequest<UpdateServiceResponse>;
}
