using Application.Infrastructure;
using Application.Photos;
using Application.Photos.Persistence;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Data.Photos;

public class PhotoRepository : IPhotoRepository
{
    protected readonly IApplicationContext _context;
    private readonly IProvideTime _timeProvider;

    public PhotoRepository(
        IApplicationContext context,
        IProvideTime timeProvider
    )
    {
        _context = context;
        _timeProvider = timeProvider;
    }

    // NOTE: this kind of stuff belongs in a base repo.
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PhotoDetailsEntity?> Find(int photoId)
    {
        return await _context.PhotoDetails.FirstOrDefaultAsync(x => x.PhotoId == photoId);
    }

    public async Task<PhotoDetailsEntity> Upsert(PhotoDetailsEntity entity)
    {
        var existingEntity = await Find(entity.PhotoId);
        var now = _timeProvider.UtcNow;

        if (existingEntity == null)
        {
            entity.CreatedOn = now;
            entity.LastUpdatedOn = now;
            _context.PhotoDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        else
        {
            existingEntity.LastUpdatedOn = now;
            existingEntity.Likes = entity.Likes;
            _context.PhotoDetails.Update(existingEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}