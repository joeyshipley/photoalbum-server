namespace Application.Infrastructure.RequestResponse;

public interface IRequest
{
    List<(string Key, string ErrorMessage)> Validate();
}