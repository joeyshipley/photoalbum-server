using Application.Photos.External;

namespace Application.Photos;

public class PhotoDetailsDto
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public int? Likes { get; set; }

    public static PhotoDetailsDto From(PhotoExternalSourceDto source)
    {
        return new PhotoDetailsDto
        {
            Id = source.Id,
            AlbumId = source.AlbumId,
            Title = source.Title,
            Url = source.Url,
            ThumbnailUrl = source.ThumbnailUrl,
        };
    }
}