namespace Application.Photos.Persistence;

public interface IPhotoRepository
{
    // NOTE: This one belongs in a base repo.
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<PhotoDetailsEntity?> Find(int photoId);
    Task<PhotoDetailsEntity> Upsert(PhotoDetailsEntity entity);
}