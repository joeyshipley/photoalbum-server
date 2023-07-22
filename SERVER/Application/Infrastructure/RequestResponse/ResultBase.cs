namespace Application.Infrastructure.RequestResponse;

public class ResultBase
{
    public List<(string Key, string ErrorMessage)> Errors { get; set; } = new();
    
    public void AddError((string Key, string ErrorMessage) error)
    {
        Errors.Add(error);
    }
    
    public void AddErrors(List<(string Key, string ErrorMessage)> errors)
    {
        Errors.AddRange(errors);
    }
}