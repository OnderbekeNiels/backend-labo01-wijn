using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_labo01_wijn.Data;
using backend_labo01_wijn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_labo01_wijn.Repositories
{
    public interface IWineRepository
    {
        Task<Wine> AddWine(Wine wine);
        Task<Wine> GetWineById(Guid wineId);
        Task<List<Wine>> GetWines();
        Task<int> RemoveWineById(Guid wineId);
        Task<int> UpdateWine(Wine wine);
    }

    public class WineRepository : IWineRepository
    {
        private IWineContext _context;

        public WineRepository(IWineContext context)
        {
            _context = context;
        }

        public async Task<List<Wine>> GetWines()
        {
            return await _context.Wines.ToListAsync();
        }

        public async Task<Wine> GetWineById(Guid wineId)
        {
            return await _context.Wines.Where(w => w.WineId == wineId).SingleOrDefaultAsync<Wine>();
        }

        public async Task<Wine> AddWine(Wine wine)
        {
            wine.WineId = Guid.NewGuid();
            await _context.Wines.AddAsync(wine);
            await _context.SaveChangesAsync();
            return wine;
        }

        public async Task<int> RemoveWineById(Guid wineId)
        {
            try
            {
                _context.Wines.Remove(new Wine() { WineId = wineId });
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public async Task<int> UpdateWine(Wine wine)
        {
            try
            {
                _context.Wines.Update(wine);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

    }
}
