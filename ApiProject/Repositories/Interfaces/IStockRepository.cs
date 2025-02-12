using ApiProject.DTOs.Stock;
using ApiProject.Models;

namespace ApiProject.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);

        Task<Stock?> DeleteAsync(int id);   

        Task<bool> StockExists(int id);

    }
}
