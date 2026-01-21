using Microsoft.EntityFrameworkCore;

class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options) { }
}

class temporary
{
    static void Main()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=TestDb;Trusted_Connection=True;");
        System.Console.WriteLine("UseSqlServer works!");
    }
}
