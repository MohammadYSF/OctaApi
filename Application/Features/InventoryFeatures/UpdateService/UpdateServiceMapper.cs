using AutoMapper;
using OctaApi.Application.Features.InventoryFeatures.AddService;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateService
{
    public sealed class UpdateServiceMapper : Profile
    {
        public UpdateServiceMapper()
        {
            CreateMap<Service, ServiceHistory>().AfterMap((s,d) =>
            {
                d.Id = Guid.NewGuid();
                d.UpdateDate = DateTime.UtcNow;
                d.ServiceId = s.Id;
            });
        }
    }
}
