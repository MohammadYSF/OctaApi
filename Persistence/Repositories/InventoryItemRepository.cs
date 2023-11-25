using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using OctaApi.Persistence.Contexts;

namespace OctaApi.Persistence.Repositories
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InventoryItem entity)
        {
            await _context.InventoryItems.AddAsync(entity);    
        }

        public void  Delete(InventoryItem entity)
        {
            _context.InventoryItems.Remove(entity);
        }

        public async Task<List<InventoryItem>> GetAllAsync()
        {
            var result = await _context.InventoryItems.AsNoTracking().ToListAsync();
           return result;
        }

        public async Task<int> GetNewCode()
        {
            if (await _context.InventoryItems.CountAsync() == 0)
                return 1;
            return await _context.InventoryItems.Select(a=> a.Code).MaxAsync() + 1;
        }

        public void Update(InventoryItem entity)
        {
            _context.InventoryItems.Update(entity);
        }
        public async Task<InventoryItem?> GetByIdAsync(Guid id)
        {
            return await _context.InventoryItems.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<InventoryItem?> GetByCodeAsync(int code)
        {
            return await _context.InventoryItems.FirstOrDefaultAsync(a => a.Code == code);
        }

      
    }
}
