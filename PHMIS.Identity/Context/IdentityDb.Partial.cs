using Microsoft.EntityFrameworkCore;

namespace PHMIS.Identity.Context
{
    public partial class IdentityDb
    {
        partial void OnModelCreatingPartial(ModelBuilder builder)
        {
            // Place for additional configuration in Identity layer
            // Example: seed roles or configure table names in future
        }
    }
}
