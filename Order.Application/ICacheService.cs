namespace Order.Application;

public interface ICacheService
{
    void Add<T>(string key, T value, TimeSpan expirationTime);
    T Get<T>(string key);
}