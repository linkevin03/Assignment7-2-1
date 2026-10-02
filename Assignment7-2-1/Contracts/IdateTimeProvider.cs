namespace Assignment7_2_1.Contracts;

public interface IdateTimeProvider
{
    DateTimeOffset UtcNow();
}