using ApiProject.Models;

namespace ApiProject.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment> GetByIdAsync(int id);
    }
}
