using System.Text.Json;
using StackExchange.Redis;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.Repository.Repositories;

public class BasketRepository:IBasketRepositories
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
       _database = redis.GetDatabase();
    }
    
    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
      var basket = await  _database.StringGetAsync(basketId);
      
      return basket.IsNullOrEmpty? null : JsonSerializer.Deserialize<CustomerBasket>(basket); ;
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
     
        var createdOrUpdateBasket = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
        if (createdOrUpdateBasket is false ) return null;
        return await GetBasketAsync(basket.Id);
        
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
       return await _database.KeyDeleteAsync(basketId);
    }
}