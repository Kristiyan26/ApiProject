using ApiProject.Data;
using ApiProject.DTOs.Comment;
using ApiProject.Models;
using ApiProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
           Comment comment = await GetByIdAsync(id); 

            if(comment == null)
            {
                return null;
            }

           _context.Comments.Remove(comment);
           await _context.SaveChangesAsync();

           return comment;
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

        public async Task<Comment> UpdateAsync(int id, Comment commentModel)
        {
            Comment comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;

            }

            comment.Title=commentModel.Title;
            comment.Content= commentModel.Content;

            await _context.SaveChangesAsync();
            return comment;


        }
    }
}
