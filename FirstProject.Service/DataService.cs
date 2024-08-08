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

        public async Task InitData(ISellerService seller,IClientService client,IStorageService storage)
        {
            Random random = new Random();
            for(int i = 0; i < 100; i++)
            {
                await storage.CreateProduct(new Product() { Name = Guid.NewGuid().ToString(), Price = random.Next(1, 1000) });
            }
            for (int i = 0; i < 10; i++)
            {
                List<Seller> sellers = new List<Seller>();
                for (int j = 0; j < 8; j++)
                {
                    await storage.AddSeller(i + 1, new Seller() { Name = Guid.NewGuid().ToString(), ShopId = i + 1 });
                }
                await storage.CreateShop(new Shop() { Name = new Guid().ToString(), Sellers = sellers });
                for(int j = 0;j < 50; j++)
                {
                    await storage.DeliverGoods(i + 1, random.Next(1, 100), 50);
                }
            }
        }

       
    }
}
