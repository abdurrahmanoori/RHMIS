using Microsoft.EntityFrameworkCore;

namespace PHMIS.Infrastructure.Context
{
    // Partial extension point for AppDbContext for identity-related customizations
    public partial class AppDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Add any Identity-related configuration to the main AppDbContext here if needed.
            // Currently left empty intentionally.
        }
    }
}
