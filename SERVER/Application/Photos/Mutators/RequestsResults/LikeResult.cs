using Application.Infrastructure.RequestResponse;

namespace Application.Photos.Mutators.RequestsResults;

public class LikeResult : ResultBase
{
    public LikeDto PhotoLikeDetails { get; set; }
}