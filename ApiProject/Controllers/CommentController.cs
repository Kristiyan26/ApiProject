using ApiProject.DTOs.Comment;
using ApiProject.Mappers;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{

    [Route("api/controller")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
                _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentRepo.GetAllAsync();

           var commentDtos = comments.Select(x =>x.ToCommentDto());

            return Ok(commentDtos);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Comment comment =await _commentRepo.GetByIdAsync(id);    

            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
    }
}
