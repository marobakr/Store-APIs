namespace Store.S_02.Core.Services.Contract;

public interface ICacheService
{
    Task setCacheAsync(string key, object response, TimeSpan expiration);

    Task<string> getCacheKeyAsync(string key);
}