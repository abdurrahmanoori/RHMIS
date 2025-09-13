using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities.Identity.Entity;

namespace PHMIS.Infrastructure.DatabaseSeeders
{
    public static class UserSeed
    {
        public static void DataSeed(ModelBuilder modelBuilder)
        {
            // Seed users with valid HospitalId (1)
            var hasher = new PasswordHasher<ApplicationUser>();
            var admin = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@local",
                NormalizedEmail = "ADMIN@LOCAL",
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Admin",
                HospitalId = 1,
                SecurityStamp = "admin-seed"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Pass@123");

            var user = new ApplicationUser
            {
                Id = 2,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@local",
                NormalizedEmail = "USER@LOCAL",
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "User",
                HospitalId = 1,
                SecurityStamp = "user-seed"
            };
            user.PasswordHash = hasher.HashPassword(user, "Pass@123");

            modelBuilder.Entity<ApplicationUser>().HasData(admin, user);
        }
    }
}
