using Data.Photos;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure;

public static class DbContextModelBuilder
{
    public static void ConfigureModels(ModelBuilder modelBuilder)
    {
        PhotoDetailsModelBuilder.Configure(modelBuilder);
    }
}