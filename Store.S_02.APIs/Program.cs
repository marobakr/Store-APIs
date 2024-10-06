using Microsoft.EntityFrameworkCore;
using Store.S_02.Core;
using Store.S_02.Core.Mapping.Products;
using Store.S_02.Core.Services.Contract;
using Store.S_02.Repository;
using Store.S_02.Repository.Data;
using Store.S_02.Repository.Data.Contexts;
using Store.S_02.Service.Services.Products;

namespace Store.S_02.APIs;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        /* === === === === === Apply DB Context === === === === ===*/
        builder.Services.AddDbContext<StoreDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        /* === === === === === Allow Dependency Injection === === === === ===*/
        builder.Services.AddScoped(typeof(IProductService), typeof(ProductsServices));
        builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddAutoMapper(M => M.AddProfile(new ProductsProfile()));
        
        var app = builder.Build();

        /* === === === === === Apply Migrations === === === === === */
        using (var scope = app.Services.CreateScope()) {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>(); /* Create Context Instance */
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); 

           try
           {
               await context.Database.MigrateAsync(); /* Apply Migrations When the Application Starts */
                await StoreDbContextSeed.SeedAsync(context); /* Seed Data to Database */

           }
           catch (Exception e)
           {
              var logger = loggerFactory.CreateLogger<Program>();
              
              logger.LogError(e, "An error occurred while migrating the database.");
           }
           
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}