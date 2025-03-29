using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiredBrainCoffee.Areas.Identity.Data;
using WiredBrainCoffee.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WiredBrainCoffeeContextConnection") ?? throw new InvalidOperationException("Connection string 'WiredBrainCoffeeContextConnection' not found.");

builder.Services.AddDbContext<WiredBrainCoffeeContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<WiredBrainCoffeeUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WiredBrainCoffeeContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expires after 30 minutes
    options.SlidingExpiration = true;                  // Optional: Extends expiration on activity
    options.Cookie.HttpOnly = true;                    // Secure against XSS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Send cookie only over HTTPS
    options.Cookie.Path = "/";
    options.Cookie.Name = "__Host-Identity";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Path = "/";
    options.Cookie.Name = "__Host-Session";
});

var app = builder.Build();

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

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
