namespace Application.Infrastructure.External;

public class ApiCallerResponse<T>
{
    public T Model { get; set; }
    public bool WasSuccessful() => !Errors.Any();
    public List<string> Errors  { get; set; } = new ();
}