using ApiProject.DTOs.Stock;
using ApiProject.Extentions;
using ApiProject.Mappers;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{

    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager,
             IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetPortfolio()
        {
            string username = User.GetUsername();

            AppUser appUser = await _userManager.FindByNameAsync(username.ToLower());

            List<StockDto> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio == null)
            {
                return BadRequest();
            }

            return Ok(userPortfolio);

        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddPortfolio(string symbol)
        {

            string username = User.GetUsername();

            AppUser appUser = await _userManager.FindByNameAsync(username.ToLower());

            Stock stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock not found!");
            }

            List<StockDto> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);


            if (userPortfolio.Any(x => x.Symbol.ToLower().Contains(stock.Symbol.ToLower())))
            {
                return BadRequest("Cannot add the same stock to the portfolio.");
            }

            Portfolio newPortfolio = new Portfolio();

            newPortfolio.StockId = stock.Id;
            newPortfolio.AppUserId = appUser.Id;

            Portfolio check = await _portfolioRepo.CreateAsync(newPortfolio);

            if (check == null)
            {
                return StatusCode(500, "Could not create.");
            }
            else
            {
                return Created();
            }

        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            string username = User.GetUsername();

            AppUser appUser = await _userManager.FindByNameAsync(username.ToLower());

            List<StockDto> portfolioToDelete =await _portfolioRepo.GetUserPortfolio(appUser);

            List<StockDto> filtered = portfolioToDelete.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filtered.Count==1)
            {
               Portfolio check = await _portfolioRepo.DeleteAsync(appUser,symbol);
                if (check == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("Stock not in your portfolio.");
            }
     

        }

    }
}
