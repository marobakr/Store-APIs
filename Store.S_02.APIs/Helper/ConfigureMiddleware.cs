using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.S_02.APIs.MiddleWears;
using Store.S_02.Core.Entities.Identity;
using Store.S_02.Repository.Data;
using Store.S_02.Repository.Data.Contexts;
using Store.S_02.Repository.Identity;
using Store.S_02.Repository.Identity.Contexts;

namespace Store.S_02.APIs.Helper;

static class ConfigureMiddleware
{

    public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>(); /* Create Context Instance */
            var IdentityDb = services.GetRequiredService<StoreIdentityDbContext>(); /* Create Context Instance */    
            var userManger = services.GetRequiredService<UserManager<AppUser>>(); /* Create Context Instance */

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync(); /* Apply Migrations When the Application Starts */
                await StoreDbContextSeed.SeedAsync(context); /* Seed Data to Database */
                await IdentityDb.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManger);

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

        app.UseMiddleware<ExceptionMiddlewear>(); // Use Exception Middlewear


        app.UseStatusCodePagesWithReExecute("/error/{0}}"); // Use Status Code Pages
        
        app.UseHttpsRedirection();// Redirect HTTP to HTTPS

        app.UseAuthorization();// Use Authorization
        app.UseAuthentication();// Use Authentication

        app.MapControllers();// Map Controllers

        app.Run();
        
        return app;
    }
}