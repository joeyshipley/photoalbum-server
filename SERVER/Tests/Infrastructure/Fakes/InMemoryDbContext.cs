using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Fakes;

public class InMemoryDbContext : ApplicationContext, IApplicationContext
{
    public InMemoryDbContext() : base(null) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TEST_DB");
    }
}