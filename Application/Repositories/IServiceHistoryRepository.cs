using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Repositories
{
    public interface IServiceHistoryRepository
    {
        Task AddAsync(ServiceHistory entity);
        Task<ServiceHistory?> GetLatestServiceHistoryByServiceIdAndDate(Guid serviceId, DateTime dateTime);
    }
}
