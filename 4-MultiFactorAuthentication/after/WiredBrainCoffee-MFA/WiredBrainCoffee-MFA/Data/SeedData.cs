using Microsoft.AspNetCore.Identity;
using WiredBrainCoffee_MFA.Areas.Identity.Data;

namespace WiredBrainCoffee_MFA.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            // Check if any users exist in the database
            var user = await userManager.FindByEmailAsync("sam@example.com");

            if (user == null)
            {
                // Create a test user if none exists
                user = new ApplicationUser
                {
                    UserName = "sam@example.com",
                    Email = "sam@example.com"
                };

                var result = await userManager.CreateAsync(user, "Password1!");

                if (!result.Succeeded)
                {
                    // Handle errors (e.g., logging or throwing an exception)
                    throw new InvalidOperationException("Could not create test user.");
                }
            }

            // Optionally, create additional test roles or users
        }
    }
}
