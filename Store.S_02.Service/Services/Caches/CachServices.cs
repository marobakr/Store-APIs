using System.Text.Json;
using StackExchange.Redis;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.Service.Services.Caches;

public class CachServices : ICacheService
{
    private readonly IDatabase _database;

    public CachServices(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task setCacheAsync(string key, object response, TimeSpan expiration)
    {
        if (response is null) return;

        var configure = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        await _database.StringSetAsync(key, JsonSerializer.Serialize(response, configure), expiration);
    }

    public async Task<string> getCacheKeyAsync(string key)
    {
        var cachResonse = await _database.StringGetAsync(key);
        if (cachResonse.IsNullOrEmpty) return null;
        return cachResonse.ToString();
    }
}