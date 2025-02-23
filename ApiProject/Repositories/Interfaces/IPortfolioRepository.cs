using ApiProject.DTOs.Stock;
using ApiProject.Models;

namespace ApiProject.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetUserPortfolio(AppUser user);

        Task<Portfolio> CreateAsync(Portfolio portfolio);

        Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol);
    }
}
