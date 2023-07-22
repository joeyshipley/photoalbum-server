using Application.Photos;
using Application.Photos.Persistence;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Data.Photos;

public class PhotoRepository : IPhotoRepository
{
    protected readonly IApplicationContext _context;

    public PhotoRepository(IApplicationContext context)
    {
        _context = context;
    }

    // NOTE: this one belongs in a base repo.
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
        var now = DateTime.UtcNow; // TODO: time provider[

        if (existingEntity == null)
        {
            entity.CreatedOn = now;
            entity.LastUpdatedOn = now;
            _context.PhotoDetails.Add(entity);
        }
        else
        {
            // TODO: confirm this works as expected.
            entity.LastUpdatedOn = now;
            _context.PhotoDetails.Update(entity);
        }

        return entity;
    }
}