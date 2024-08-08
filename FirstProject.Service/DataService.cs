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
    /// <summary>
    /// 
    /// </summary>
    public class DataService : IDataModulService
    {
        /// <summary>
        /// 
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>

        public DataService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Buyer> GetRandomBuyer()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var buyers = await db.Buyers.ToListAsync();
            var buyer = buyers[random.Next(0, buyers.Count-1)];
            return buyer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Product> GetRandomProduct()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var products = await db.Products.ToListAsync();
            var product = products[random.Next(0, products.Count - 1)];
            return product;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task DelleteAll()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.ToListAsync();
            db.Shops.RemoveRange(shops);
            var seller = await db.Sellers.ToListAsync();
            db.Sellers.RemoveRange(seller);
            var store = await db.StorageTransactions.ToListAsync();
            db.StorageTransactions.RemoveRange(store);
            var buyer = await db.Buyers.ToListAsync();
            db.Buyers.RemoveRange(buyer);
            var order = await db.Orders.ToListAsync();
            db.Orders.RemoveRange(order);
            var orderItem = await db.OrderItems.ToListAsync();
            db.OrderItems.RemoveRange(orderItem);
            var orderTransact = await db.OrderTransactions.ToListAsync();
            db.OrderTransactions.RemoveRange(orderTransact);
            var product = await db.Products.ToListAsync();
            db.Products.RemoveRange(product);
            await db.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Seller> GetRandomSeller()
        {

            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var sellers = await db.Sellers.ToListAsync();
            var seller = sellers[random.Next(0, sellers.Count - 1)];
            return seller;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public async Task InitData(IStorageService storage)
        {
            Random random = new Random();
            for(int i = 0; i < 100; i++)
            {
                await storage.CreateProduct(new Product() { Name = Guid.NewGuid().ToString(), Price = random.Next(1, 1000) });
            }
            List<Buyer> buyerList = new List<Buyer>();
            for(int j=0; j<10; j++)
            {
                buyerList.Add(new Buyer {Name = Guid.NewGuid().ToString(),});
            }
            await storage.AddBuyer(buyerList);

            for (int i = 0; i < 10; i++)
            {
                List<Seller> sellers = new List<Seller>();
                List<StorageTransaction> transactions = new List<StorageTransaction>();
                var shops = await storage.GetShops();
                


               for (int j = 0; j < 8; j++)
                {
                    sellers.Add(new Seller() { Name = new Guid().ToString() });
                }
                for (int j = 0; j < 50; j++)
                {

                    transactions.Add(new StorageTransaction() { ProductId =(await GetRandomProduct()).Id, Count = 50});
                }
                await storage.CreateShop(new Shop() { Name = Guid.NewGuid().ToString(), Sellers = sellers, Transactions = transactions});

               
            }
        }


    }
}
