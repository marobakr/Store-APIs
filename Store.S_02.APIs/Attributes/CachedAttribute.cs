using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.APIs.Attributes;

public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _expireTime;

    public CachedAttribute(int expireTime)
    {
        _expireTime = expireTime;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cachedService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
        var cacheresponse = await cachedService.getCacheKeyAsync(cacheKey);
        if (!string.IsNullOrEmpty(cacheresponse))
        {
            var contentResult = new ContentResult()
            {
                Content = cacheresponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = contentResult;
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult response)
        {
            await cachedService.setCacheAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
        }
    }

    private string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var cachedKey = new StringBuilder();
        cachedKey.Append($"{request.Path}");
        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            cachedKey.Append($"|{key}-{value}");
        }

        return cachedKey.ToString();
    }
}