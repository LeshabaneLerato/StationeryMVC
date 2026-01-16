using Microsoft.EntityFrameworkCore;
using StationeryMVC.Models;

namespace StationeryMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Automatically create the database and tables if they do not exist
            Database.EnsureCreated();
        }

        public DbSet<StationeryItem> StationeryItems { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItem> QuotationItems { get; set; }
    }
}
