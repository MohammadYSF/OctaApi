using AutoMapper;
using OctaApi.Application.DomainModels;
using OctaApi.Domain.Models;
namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;

public class AddCustomerMapper:Profile
{
    public AddCustomerMapper()
    {
        CreateMap<VehicleDTO, Vehicle>().ForMember(dest => dest.Code, opt => opt.Ignore());            
        CreateMap<AddCustomerRequest, Customer>().AfterMap((s, d) =>
        {
            d.Id = Guid.NewGuid();
            d.IsActive = true;
        });
    }
}
