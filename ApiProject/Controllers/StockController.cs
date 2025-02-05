using ApiProject.Data;
using ApiProject.DTOs.Stock;
using ApiProject.Mappers;
using ApiProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context; 
        public StockController(ApplicationDbContext context)
        {            
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<StockDto> stocks = _context.Stocks.Select(s=>s.ToStockDto()).ToList();

            return Ok(stocks);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Stock stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if(stock!=null)
            {
                return Ok(stock.ToStockDto());
            }
            else
            {
                return NotFound ();    
            }
             
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            Stock stockModel = stockDto.ToStockFromCreateDto();
            _context.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById),new { id = stockModel.Id },stockModel.ToStockDto());   
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            Stock stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
           
                stockModel.Symbol = updateDto.Symbol;
                stockModel.MarketCap = updateDto.MarketCap;
                stockModel.CompanyName = updateDto.CompanyName;
                stockModel.Purchase = updateDto.Purchase;
                stockModel.LastDiv = updateDto.LastDiv;
                stockModel.Industry = updateDto.Industry;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Stock stockToDelete = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockToDelete == null)
            {
                return NotFound();
            }
            _context.Remove(stockToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
