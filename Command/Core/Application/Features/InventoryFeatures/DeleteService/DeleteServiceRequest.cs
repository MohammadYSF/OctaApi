using MediatR;

namespace Command.Core.Application.Features.InventoryFeatures.DeleteService;

public sealed record DeleteServiceRequest(int Code):IRequest<DeleteServiceResponse>;    
