using ApiProject.Data;
using ApiProject.DTOs.Stock;
using ApiProject.Mappers;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
    
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context,IStockRepository stockRepo)
        {            
            _stockRepo = stockRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Stock> stocks = await _stockRepo.GetAllAsync();

            var result = stocks.Select(s => s.ToStockDto());


            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Stock stock = await _stockRepo.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            Stock stockModel = stockDto.ToStockFromCreateDto();

            await _stockRepo.CreateAsync(stockModel);   
        

            return CreatedAtAction(nameof(GetById),new { id = stockModel.Id },stockModel.ToStockDto());   
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            Stock stockModel = await _stockRepo.UpdateAsync(id,updateDto);  
            if (stockModel == null)
            {
                return NotFound();
            }   

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Stock stockToDelete = await _stockRepo.DeleteAsync(id);
            if (stockToDelete == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
