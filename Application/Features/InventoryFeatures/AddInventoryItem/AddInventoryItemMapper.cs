using AutoMapper;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddInventoryItem
{
    public sealed class AddInventoryItemMapper : Profile
    {
        public AddInventoryItemMapper()
        {
            CreateMap<AddInventoryItemRequest, InventoryItem>().AfterMap((s,d) =>
            {
                d.Id = Guid.NewGuid();
                d.IsActive = true;
                d.RegisterDate = DateTime.Now;
                d.Count = 0;
            });
            CreateMap<AddInventoryItemRequest, InventoryItemHistory>().AfterMap((s, d) =>
            {
                d.Id = Guid.NewGuid();
                d.IsActive = true;
                d.Count = 0;
                d.UpdateDate = DateTime.Now;
            }); ;
        }
    }
}
