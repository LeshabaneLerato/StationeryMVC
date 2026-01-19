using Microsoft.EntityFrameworkCore;
using StationeryMVC.Data;
using StationeryMVC.Models;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ====================
// SERVICES
// ====================
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext (ONLY ONE DB CONTEXT)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ====================
// BUILD THE APP
// ====================
var app = builder.Build();

// ====================
// STEP 1.3: SEED SHOP SETTINGS
// ====================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Apply migrations automatically
    context.Database.Migrate();

    // Seed default shop details if not exists
    if (!context.AppSettings.Any())
    {
        context.AppSettings.Add(new AppSettings
        {
            ShopName = "Triple L Stationery Shop",
            Slogan = "Everything You Need for School & Office",
            LogoPath = "/images/logo.png"
        });

        context.SaveChanges();
    }
}

// ====================
// ROTATIVA CONFIGURATION
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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ====================
// ROUTES
// ====================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stationery}/{action=Index}/{id?}"
);

// ====================
// RUN THE APP
// ====================
app.Run();
