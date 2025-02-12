using ApiProject.Data;
using ApiProject.DTOs.Stock;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repositories
{
    public class StockRepository : IStockRepository
    {
       private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
                _context= context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);    
           await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            Stock stockModel= await _context.Stocks.FirstOrDefaultAsync(x=> x.Id==id);

            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(x=>x.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(x => x.Comments).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            Stock stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id== id);   
            
            if(stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = stockDto.Symbol;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;

            await _context.SaveChangesAsync();
            return stockModel;  
        }
    }
}
