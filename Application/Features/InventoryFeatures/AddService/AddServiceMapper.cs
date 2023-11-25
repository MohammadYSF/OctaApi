using AutoMapper;
using OctaApi.Application.Features.InventoryFeatures.AddInventoryItem;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddService
{
    public sealed class AddServiceMapper: Profile
    {
        public AddServiceMapper()
        {
            CreateMap<AddServiceRequest, Service>().AfterMap((s,d) =>
            {
                d.Id = Guid.NewGuid();
                d.RegisterDate = DateTime.UtcNow;
            });
            CreateMap<AddServiceRequest, ServiceHistory>().AfterMap((s, d) =>
            {
                d.Id = Guid.NewGuid();
                d.UpdateDate = DateTime.UtcNow;
            });
        }
    }
}
