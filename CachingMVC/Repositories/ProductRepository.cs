using CachingMVC.Models;
using CachingMVC.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingMVC.Repositories
{
    public class ProductRepository
    {
        private MobileContext db;
        public ProductRepository(MobileContext context)
        {
            db = context;
        }

        public void Initialize()
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(
                    new Product { Name = "iPhone 8", Company = "Apple", Price = 600 },
                    new Product { Name = "Galaxy S9", Company = "Samsung", Price = 550 },
                    new Product { Name = "Pixel 2", Company = "Google", Price = 500 }
                );
                db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            await db.Products.AddAsync(product);
            db.SaveChanges();

            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }
    }
}
