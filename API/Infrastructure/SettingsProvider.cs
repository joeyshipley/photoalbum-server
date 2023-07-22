using Application.Infrastructure;

namespace API.Infrastructure;

public class SettingsProvider : ISettingsProvider
{
    private readonly IConfiguration _configuration;

    public SettingsProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? DbConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }
}