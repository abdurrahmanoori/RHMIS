using Microsoft.EntityFrameworkCore;

namespace RHMIS.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

       
    }
}
