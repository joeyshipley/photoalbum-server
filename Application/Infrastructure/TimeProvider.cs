namespace Application.Infrastructure;

public interface IProvideTime
{
    public DateTime UtcNow { get; }
}

public class TimeProvider : IProvideTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}