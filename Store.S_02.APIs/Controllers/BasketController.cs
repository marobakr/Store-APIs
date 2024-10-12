using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.S_02.APIs.Error;
using Store.S_02.Core.Dtos.Basket;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.APIs.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepositories _basketRepository;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepositories basketRepository , IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }
  
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket (string id)
    {
        if (id is null) return BadRequest(new APiErrorResponse(400, "invalid Id"));
        var basket = await _basketRepository.GetBasketAsync(id);
        if (basket is null) new CustomerBasket() { Id = id };
        return Ok(basket);
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket (CustomerBasketDto model)
    {
        var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));
        if (basket is null) return BadRequest(new APiErrorResponse(400, "Problem updating the basket"));
        return Ok(basket);
    }
   
    [HttpDelete]
    public async Task DeleteBasket (string id)
    {
          await _basketRepository.DeleteBasketAsync(id);
    }
    
}