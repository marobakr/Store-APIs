using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.S_02.APIs.Error;
using Store.S_02.Core;
using Store.S_02.Core.Mapping.Basket;
using Store.S_02.Core.Mapping.Products;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Repository;
using Store.S_02.Repository.Data.Contexts;
using Store.S_02.Repository.Repositories;
using Store.S_02.Service.Services.Products;

namespace Store.S_02.APIs.Helper;

public static class DependancyInjection
{
    public static IServiceCollection AddDE(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBuiltInService();
        services.AddSwaggerService();
        services.AddDbContextService(configuration);
        services.AddUserDefinedService();
        services.AddAutoMapperService(configuration);
        services.ConfigrationsInvalidModelStateResponseServices();
        services.AddRedisService(configuration);
        
        return services;
    }
    
    /* === === === === === Add Built-In Service === === === === ===*/
    private static IServiceCollection AddBuiltInService(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
    
    /* === === === === === Add Swagger Service === === === === ===*/
    private static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
   
    /* === === === === === Apply DB Context === === === === ===*/
    private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        return services;
    }
    
    /* === === === === === User Defined Service === === === === ===*/
    private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductService), typeof(ProductsServices));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IBasketRepositories), typeof(BasketRepository));

        
        return services;
    }
    
   
    /* === === === === === Add Auto Mapper === === === === ===*/
    private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAutoMapper(M => M.AddProfile(new ProductsProfile())); // configuration
        services.AddAutoMapper(M => M.AddProfile(new BasketProfile())); // configuration

        return services;
    }

     
    /* === === === === === Configurations Invalid Model State ResponseServices === === === === ===*/
    private static IServiceCollection ConfigrationsInvalidModelStateResponseServices(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => {
            options.InvalidModelStateResponseFactory = (ActionContext) =>
            {
                var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(P => P.ErrorMessage).ToArray();

                var response = new ApiValidationResponse()
                {
                    Erros = errors
                };
                return new BadRequestObjectResult(response);
            };
        });
        return services;
    }

    private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
        {
         var connection =   configuration.GetConnectionString("Redis");
         return ConnectionMultiplexer.Connect(connection);
        });
        return services;
    }
}