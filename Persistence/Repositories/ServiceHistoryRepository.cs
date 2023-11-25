using Microsoft.EntityFrameworkCore;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using OctaApi.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Persistence.Repositories
{
    public class ServiceHistoryRepository : IServiceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ServiceHistory entity)
        {
            await _context.ServiceHistories.AddAsync(entity);
        }

        public async Task<ServiceHistory?> GetLatestServiceHistoryByServiceIdAndDate(Guid serviceId, DateTime dateTime)
        {
            return await _context.ServiceHistories.Where(a => a.ServiceId == serviceId && a.UpdateDate <= dateTime)
                .OrderByDescending(a => a.UpdateDate).FirstOrDefaultAsync();
        }
    }
}
