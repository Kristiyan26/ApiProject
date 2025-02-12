using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
           
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            Comment comment =await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return null;
            }
            return comment;
        }
    }
}
