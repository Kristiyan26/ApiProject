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
    public class CommentController : ControllerBase
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Comment> comments = await _commentRepo.GetAllAsync();

            var commentDtos = comments.Select(x => x.ToCommentDto());

            return Ok(commentDtos);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comment comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,
                                                [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, 
                                                [FromBody] UpdateCommentDto commentDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comment commentModel = await _commentRepo.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());

            if(commentModel == null)
            {
                return NotFound("Comment not found!");
            }

            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comment commentModel = await _commentRepo.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound("Comment not found!");
            }
            return NoContent();
        }
    }
}

