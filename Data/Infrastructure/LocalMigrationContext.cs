using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure;

public class LocalMigrationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: come back to this when you aren't brain-dead tired...
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=LTPA_DEV;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}