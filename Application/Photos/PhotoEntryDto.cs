using Application.Photos.External;

namespace Application.Photos;

public class PhotoEntryDto
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string? Title { get; set; }
    public string? ThumbnailUrl { get; set; }

    public static PhotoEntryDto From(PhotoExternalSourceDto source)
    {
        return new PhotoEntryDto
        {
            Id = source.Id,
            AlbumId = source.AlbumId,
            Title = source.Title,
            ThumbnailUrl = source.ThumbnailUrl,
        };
    }
}