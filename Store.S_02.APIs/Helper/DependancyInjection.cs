using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Store.S_02.APIs.Error;
using Store.S_02.Core;
using Store.S_02.Core.Entities.Identity;
using Store.S_02.Core.Mapping.Auth;
using Store.S_02.Core.Mapping.Basket;
using Store.S_02.Core.Mapping.Products;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Repository;
using Store.S_02.Repository.Data.Contexts;
using Store.S_02.Repository.Identity.Contexts;
using Store.S_02.Repository.Repositories;
using Store.S_02.Service.Services.Caches;
using Store.S_02.Service.Services.Products;
using Store.S_02.Service.Services.Token;
using Store.S_02.Service.Services.Users;

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
        services.AddIdentityService();
        services.AddAuthService(configuration);

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
    private static IServiceCollection AddDbContextService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDbContext<StoreIdentityDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });


        return services;
    }

    /* === === === === === User Defined Service === === === === ===*/
    private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductService), typeof(ProductsServices));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IBasketRepositories), typeof(BasketRepository));
        services.AddScoped(typeof(ICacheService), typeof(CachServices));
        services.AddScoped(typeof(ITokenService), typeof(TokenService));
        services.AddScoped(typeof(IUserService), typeof(UserService));

        return services;
    }


    /* === === === === === Add Auto Mapper === === === === ===*/
    private static IServiceCollection AddAutoMapperService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(M => M.AddProfile(new ProductsProfile())); // configuration
        services.AddAutoMapper(M => M.AddProfile(new BasketProfile())); // configuration
        services.AddAutoMapper(M => M.AddProfile(new AuthProfile())); // configuration

        return services;
    }


    /* === === === === === Configurations Invalid Model State ResponseServices === === === === ===*/
    private static IServiceCollection ConfigrationsInvalidModelStateResponseServices(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
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
            var connection = configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(connection);
        });
        return services;
    }

    private static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();
        return services;
    }

    private static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(
            option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        return services;
    }
}