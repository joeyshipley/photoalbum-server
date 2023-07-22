namespace Application.Infrastructure;

public interface ISettingsProvider
{
    string? DbConnectionString();
}
