using Assignment7_2_1.Contracts;

namespace Assignment7_2_1.Service;

public class FixedDateTimeProvider : IDateTimeProvider
{
    private readonly DateTimeOffset _fixedTime;

    public FixedDateTimeProvider(DateTimeOffset fixedTime)
    {
        _fixedTime = fixedTime;
    }
    
    public DateTimeOffset UtcNow() => _fixedTime;
}