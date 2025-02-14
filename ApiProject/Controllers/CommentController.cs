using ApiProject.DTOs.Comment;
using ApiProject.Mappers;
using ApiProject.Models;
using ApiProject.Repositories;
using ApiProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace ApiProject.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;


        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentRepo.GetAllAsync();

            var commentDtos = comments.Select(x => x.ToCommentDto());

            return Ok(commentDtos);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,
                                                [FromBody] CreateCommentDto commentDto)
        {
            bool stockExists = await _stockRepo.StockExists(stockId);

            if (!stockExists)
            {
                return BadRequest("Stock does not exist!");
            }

            Comment comment = commentDto.ToCommentFromCreateDto(stockId);

            await _commentRepo.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());



        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, 
                                                [FromBody] UpdateCommentDto commentDto )
        {
            Comment commentModel = await _commentRepo.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());

            if(commentModel == null)
            {
                return NotFound("Comment not found!");
            }

            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Comment commentModel = await _commentRepo.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound("Comment not found!");
            }
            return NoContent();
        }
    }
}

