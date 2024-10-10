using Store.S_02.Core.Entities;

namespace Store.S_02.Core.Services.Contract;

public interface IBasketRepositories
{
  Task<CustomerBasket?> GetBasketAsync (string basketId);
  Task<CustomerBasket?> UpdateBasketAsync (CustomerBasket basket);
  Task<bool> DeleteBasketAsync (string basketId);


}