namespace Application.Infrastructure.RequestResponse;

public class ResultBase
{
    public List<(string Key, string Text)> Errors { get; set; } = new();
    
    public void AddError((string Key, string Text) error)
    {
        Errors.Add(error);
    }
    
    public void AddErrors(List<(string Key, string Text)> errors)
    {
        Errors.AddRange(errors);
    }
}