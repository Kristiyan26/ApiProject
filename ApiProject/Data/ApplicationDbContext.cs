using ApiProject.DTOs.Stock;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ApiProject.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().HasData(
                new Stock { Id = 1, Symbol = "AAPL", CompanyName = "Apple Inc.", Purchase = 145.30m, LastDiv = 0.82m, Industry = "Technology", MarketCap = 2230000000000, Comments = new List<Comment>() },
                new Stock { Id = 2, Symbol = "MSFT", CompanyName = "Microsoft Corporation", Purchase = 265.65m, LastDiv = 0.56m, Industry = "Technology", MarketCap = 1980000000000, Comments = new List<Comment>() }
            );

            modelBuilder.Entity<Comment>().HasData(
                 new Comment { Id = 1, Title = "Great Stock", Content = "Apple stocks are performing exceptionally well.", CreateOn = new DateTime(2023, 1, 1), StockId = 1 },
                 new Comment { Id = 2, Title = "Steady Growth", Content = "Microsoft has shown consistent growth.", CreateOn = new DateTime(2023, 1, 2), StockId = 2 }
             );

            base.OnModelCreating(modelBuilder);
        }

        internal async Task<List<StockDto>> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
