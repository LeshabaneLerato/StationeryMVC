using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using StationeryMVC.Data;
using StationeryMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// ====================
// SERVICES
// ====================
builder.Services.AddControllersWithViews();

// ✅ Add EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ✅ Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// ====================
// APPLY MIGRATIONS & SEED SETTINGS
// ====================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.AppSettings.Any())
    {
        context.AppSettings.Add(new AppSettings
        {
            ShopName = "Triple L Stationery Shop",
            Slogan = "Everything You Need for School & Office",
            LogoPath = "/images/TripleL.png"
        });
        context.SaveChanges();
    }
}

// ====================
// ROTATIVA CONFIG
// ====================
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// ====================
// MIDDLEWARE
// ====================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Create Admin role
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // Create default admin user
    string adminEmail = "admin@triplel.com";
    string adminPassword = "Lerato@91";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stationery}/{action=Index}/{id?}"
);

app.Run();
