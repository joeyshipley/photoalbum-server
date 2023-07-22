using Application.Infrastructure;

namespace Tests.Infrastructure.Fakes;

public class FakeTimeProvider : IProvideTime
{
    private DateTime _time = DateTime.UtcNow;

    public void SetTime(DateTime time)
    {
        _time = time; 
    }

    public DateTime UtcNow => _time;
}