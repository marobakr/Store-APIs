using System.Text.Json;
using Store.S_02.Core.Entities;
using Store.S_02.Repository.Data.Contexts;

namespace Store.S_02.Repository.Data;

public class StoreDbContextSeed
{
    public static async Task SeedAsync(StoreDbContext _context)
    {
        /* Brand */
        if (_context.Brands.Count() == 0)
        {
            // * 1: Read Data from JSON File
            var brandsData = await File.ReadAllTextAsync(@"../Store.S_02.Repository/Data/SeedData/brands.json");

            // * 2: Convert JSON String to List of ProductBrand
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            // * 3: Seed Data to Database

            if (brands != null && brands.Count() > 0) await _context.Brands.AddRangeAsync(brands);
        }

        /* Types */
        if (_context.Type.Count() == 0)
        {
            // * 1: Read Data from JSON File
            var typesData = await File.ReadAllTextAsync(@"../Store.S_02.Repository/Data/SeedData/types.json");

            // * 2: Convert JSON String to List of ProductBrand
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            // * 3: Seed Data to Database

            if (types != null && types.Count() > 0) await _context.Type.AddRangeAsync(types);
        }

        /* Products */
        if (_context.Products.Count() == 0)
        {
            // * 1: Read Data from JSON File
            var productsData = await File.ReadAllTextAsync(@"../Store.S_02.Repository/Data/SeedData/products.json");

            // * 2: Convert JSON String to List of ProductBrand
            var product = JsonSerializer.Deserialize<List<Products>>(productsData);

            // * 3: Seed Data to Database

            if (product != null && product.Count() > 0)
            {
                await _context.Products.AddRangeAsync(product);
            }
        }
        
        await _context.SaveChangesAsync(); //* Save Changes

    }
}