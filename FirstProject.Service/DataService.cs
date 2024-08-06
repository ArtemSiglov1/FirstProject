using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    public class DataService : IDataModulService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public DataService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<Buyer> GetRandomBuyer()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var buyers = await db.Buyers.ToListAsync();
            var buyer = buyers[random.Next(0, buyers.Count-1)];
            return buyer;
        }

        public async Task<Product> GetRandomProduct()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var products = await db.Products.ToListAsync();
            var product = products[random.Next(0, products.Count - 1)];
            return product;
        }

        public async Task<Seller> GetRandomSeller()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var sellers = await db.Sellers.ToListAsync();
            var seller = sellers[random.Next(0, sellers.Count - 1)];
            return seller;
        }
    }
}
