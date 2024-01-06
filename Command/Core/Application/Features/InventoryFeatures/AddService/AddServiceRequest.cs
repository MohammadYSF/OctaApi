using MediatR;
namespace Command.Core.Application.Features.InventoryFeatures.AddService;
public sealed record AddServiceRequest(string Name, long DefaultPrice) : IRequest<AddServiceResponse>;
