using Application.Photos;

namespace Tests.Infrastructure.Creationals.Entities;

// NOTE: Contrived example
public class PhotoDetailsBuilder
{
    private PhotoDetailsEntity _baseline;
    private DateTime? _createdOn;
    private int? _id;
    private int? _photoId;
    private int? _likes;
        
    public PhotoDetailsBuilder WithId(int value)
    {
        _id = value;
        return this;
    }

    public PhotoDetailsBuilder WithLikes(int value)
    {
        _likes = value;
        return this;
    }

    public PhotoDetailsBuilder WithCreationDate(DateTime value)
    {
        _createdOn = value;
        return this;
    }
    
    public PhotoDetailsBuilder WithPhotoId(int value)
    {
        _photoId = value;
        return this;
    }
    
    public PhotoDetailsBuilder WithoutDates()
    {
        _baseline.CreatedOn = default;
        _baseline.LastUpdatedOn = default;
        return this;
    }

    public PhotoDetailsEntity Build()
    {
        if(_id.HasValue)
            _baseline.Id = _id.Value;

        if(_photoId.HasValue)
            _baseline.PhotoId = _photoId.Value;

        if(_likes.HasValue)
            _baseline.Likes = _likes.Value;

        if(_createdOn.HasValue)
            _baseline.CreatedOn = _createdOn.Value;

        return _baseline;
    }

    private void SeedDefaultValues()
    {
        _baseline = new PhotoDetailsEntity
        {
            Likes = 99,
            CreatedOn = DateTime.UtcNow,
            LastUpdatedOn = DateTime.UtcNow
        };
    }
    
    public static PhotoDetailsBuilder AsDefault()
    {
        var builder = new PhotoDetailsBuilder();
        builder.SeedDefaultValues();
        return builder;
    }
}