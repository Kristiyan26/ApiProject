using ApiProject.Models;

namespace ApiProject.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
