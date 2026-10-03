using Assignment7_2_1.Contracts;

namespace Assignment7_2_1.Service;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow() => DateTimeOffset.UtcNow; 
}