using Microsoft.AspNetCore.Identity;

namespace PHMIS.Domain.Entities.Identity.Entity
{
    public class ApplicationRole : IdentityRole<int>
    {
        //[ForeignKey(nameof())]
        //public ApplicationUser? ApplicationUser { get; set; }
    }
}
