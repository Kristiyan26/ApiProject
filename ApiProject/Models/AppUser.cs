using Microsoft.AspNetCore.Identity;

namespace ApiProject.Models
{
    public class AppUser : IdentityUser
    {

        public List<Portfolio> Portfolios { get; set; }
        
    }
}
