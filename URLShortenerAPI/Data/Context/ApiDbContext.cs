using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Data.Entities;
namespace URLShortenerAPI.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<URLShortener> Url { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
