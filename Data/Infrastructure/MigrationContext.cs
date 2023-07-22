using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Infrastructure;

public interface IMigrationContext
{
    Task Migrate();
}

public class MigrationContext : DbContext, IMigrationContext
{
    private readonly bool _hasContextOptions;

    public MigrationContext()
    {
        _hasContextOptions = false;
    }

    public MigrationContext(DbContextOptions options)
        : base(options)
    {
        _hasContextOptions = true;
    }

    // TODO: no bueno revisit this, forgot how painful EF migrations were.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(_hasContextOptions)
            return;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.local.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        DbContextModelBuilder.ConfigureModels(modelBuilder);

    public async Task Migrate() => await Database.MigrateAsync();
}