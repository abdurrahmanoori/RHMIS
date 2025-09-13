
using Microsoft.AspNetCore.Identity;

namespace PHMIS.Domain.Entities.Identity.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        //public List<ApplicationRole>? ApplicationRoles { get; set; } 
    }
}
