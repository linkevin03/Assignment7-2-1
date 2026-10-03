namespace Assignment7_2_1.Contracts;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow();
}