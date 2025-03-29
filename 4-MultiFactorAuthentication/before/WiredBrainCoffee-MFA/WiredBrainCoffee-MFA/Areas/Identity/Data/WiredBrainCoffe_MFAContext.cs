using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WiredBrainCoffee_MFA.Areas.Identity.Data;

namespace WiredBrainCoffee_MFA.Data;

public class WiredBrainCoffee_MFAContext : IdentityDbContext<ApplicationUser>
{
    public WiredBrainCoffee_MFAContext(DbContextOptions<WiredBrainCoffee_MFAContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
