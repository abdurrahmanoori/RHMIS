using Microsoft.AspNetCore.Identity;

namespace PHMIS.Identity.Entity
{
    public class ApplicationRole : IdentityRole<int>
    {
        //[ForeignKey(nameof())]
        //public ApplicationUser? ApplicationUser { get; set; }
    }
}
