using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiredBrainCoffee_MFA.Areas.Identity.Data;
using WiredBrainCoffee_MFA.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WiredBrainCoffee_MFAContextConnection") ?? throw new InvalidOperationException("Connection string 'WiredBrainCoffee_MFAContextConnection' not found.");

builder.Services.AddDbContext<WiredBrainCoffee_MFAContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<WiredBrainCoffee_MFAContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var context = services.GetRequiredService<WiredBrainCoffee_MFAContext>();

    // Ensure the database is created and seed test data
    SeedData.Initialize(services, userManager).Wait();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
