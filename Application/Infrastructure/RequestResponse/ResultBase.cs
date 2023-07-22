namespace Application.Infrastructure.RequestResponse;

public class ResultBase
{
    public List<(string Key, string Text)> Errors { get; set; } = new();
}