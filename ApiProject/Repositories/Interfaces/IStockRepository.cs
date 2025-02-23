using ApiProject.DTOs.Stock;
using ApiProject.Helpers;
using ApiProject.Models;

namespace ApiProject.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);

        Task<Stock?> DeleteAsync(int id);   

        Task<bool> StockExists(int id);

        Task<Stock?> GetBySymbolAsync(string symbol);
    }
}
