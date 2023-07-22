using Application.Infrastructure;
using Application.Photos;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure;

public interface IApplicationContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<PhotoDetailsEntity> PhotoDetails { get; }
}

public class ApplicationContext : DbContext, IApplicationContext
{
    private readonly ISettingsProvider _settingsProvider;

    public ApplicationContext(ISettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _settingsProvider.DbConnectionString();
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        DbContextModelBuilder.ConfigureModels(modelBuilder);

    public DbSet<PhotoDetailsEntity> PhotoDetails => Set<PhotoDetailsEntity>();
}