using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.S_02.APIs.Error;
using Store.S_02.APIs.Helper;
using Store.S_02.APIs.MiddleWears;
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
        builder.Services.AddDE(builder.Configuration);

        var app = builder.Build();

       await app.ConfigureMiddlewaresAsync();
        
        app.Run();

    }
}