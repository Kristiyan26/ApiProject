using ApiProject.Data;
using ApiProject.DTOs.Stock;
using ApiProject.Mappers;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly ApplicationDbContext _context; 
        public PortfolioRepository(ApplicationDbContext context)
        {
                _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeleteAsync(AppUser appUser, string symbol)
        {
            Portfolio portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id
                                                                             && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if(portfolioModel == null)
            {
                return null;
            }
            else
            {
                _context.Portfolios.Remove(portfolioModel); 
                await _context.SaveChangesAsync();
            }
            return portfolioModel;
        }

        public async Task<List<StockDto>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(x => x.AppUserId == user.Id).Select(stock => new StockDto
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv=stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,    
                MarketCap =stock.Stock.MarketCap,
                Comments=stock.Stock.Comments.Select(x=>x.ToCommentDto()).ToList()
              

            }).ToListAsync();
        }
    }
}
