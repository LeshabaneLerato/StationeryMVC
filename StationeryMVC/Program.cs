using Microsoft.EntityFrameworkCore;
using StationeryMVC.Data;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ====================
// SERVICES
// ====================
builder.Services.AddControllersWithViews();

// Register the ApplicationDbContext to connect with the SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ====================
// BUILD THE APP
// ====================
var app = builder.Build();

// ====================
// ROTATIVA CONFIGURATION
// ====================
// Rotativa requires the path to wkhtmltopdf, which is necessary for PDF generation.
// Here we configure it to point to the folder where wkhtmltopdf.exe is located.

// Check if the environment is development or production to adjust the configuration accordingly
string rotativaPath = "wwwroot/Rotativa"; // Define the path to the Rotativa folder containing wkhtmltopdf.exe

// Ensures the Rotativa setup is correct by pointing to the wkhtmltopdf executable
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IWebHostEnvironment)app.Environment rotativaPath
);

// ====================
// MIDDLEWARE
// ====================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Ensures secure connections with HTTP Strict Transport Security (HSTS) in production
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files (images, JS, CSS, etc.)
app.UseRouting(); // Enable routing for controllers and actions
app.UseAuthorization(); // Enable authorization

// ====================
// ROUTES
// ====================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stationery}/{action=Index}/{id?}");

// ====================
// RUN THE APP
// ====================
app.Run();
