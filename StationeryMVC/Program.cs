using Microsoft.EntityFrameworkCore;
using StationeryMVC.Data;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core with SQL Server (change connection string as needed)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stationery}/{action=Index}/{id?}");

// Add this before app.Run()
RotativaConfiguration.Setup("wwwroot", "Rotativa");

app.Run();
