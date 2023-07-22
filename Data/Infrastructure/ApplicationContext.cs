using Application.Infrastructure;
using Application.Photos;
using Data.Photos;
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
        optionsBuilder.UseSqlServer(_settingsProvider.DbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        PhotoDetailsModelBuilder.Configure(modelBuilder);
    }

    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<PhotoDetailsEntity> PhotoDetails => Set<PhotoDetailsEntity>();
}