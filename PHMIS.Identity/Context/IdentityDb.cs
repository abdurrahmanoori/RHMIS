using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PHMIS.Identity.Entity;

namespace PHMIS.Identity.Context
{
    public partial class IdentityDb : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityDb(DbContextOptions<IdentityDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder builder);
    }
}
