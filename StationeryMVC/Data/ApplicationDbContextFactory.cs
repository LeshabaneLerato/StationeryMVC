using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StationeryMVC.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Make sure server matches your LocalDB instance
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StationeryDB;Trusted_Connection=True;MultipleActiveResultSets=true");


        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
