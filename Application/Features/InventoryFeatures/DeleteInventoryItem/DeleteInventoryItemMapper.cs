using AutoMapper;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem
{
    public sealed class DeleteInventoryItemMapper : Profile
    {
        public DeleteInventoryItemMapper()
        {
            CreateMap<InventoryItem, InventoryItemHistory>().AfterMap((s, d) =>
            {
                d.Id = Guid.NewGuid();
                d.UpdateDate= DateTime.UtcNow;
                d.InventoryItemId = s.Id;
            });
        }
    }
}
