using AutoMapper;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.DeleteService
{
    public sealed class DeleteServiceMapper : Profile
    {
        public DeleteServiceMapper()
        {
            CreateMap<Service, ServiceHistory>().AfterMap((s,d) =>
            {
                d.UpdateDate = DateTime.UtcNow;
                d.ServiceId = s.Id;
                d.Id = Guid.NewGuid();
            });
        }
    }
}
