using Application.Photos;
using Microsoft.EntityFrameworkCore;

namespace Data.Photos;

public static class PhotoDetailsModelBuilder
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoDetailsEntity>(entity =>
        {
            entity.ToTable("PhotoDetails");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.PhotoId).IsUnique();

            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.PhotoId)
                  .IsRequired()
                  .ValueGeneratedNever();

            entity.Property(e => e.Likes)
                  .IsRequired();

            entity.Property(e => e.CreatedOn)
                  .IsRequired();

            entity.Property(e => e.LastUpdatedOn)
            .IsRequired();
        });

    }
}