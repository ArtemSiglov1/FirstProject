using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using FirstProject.Data.Enums;
using Microsoft.IdentityModel.Tokens;
using FirstProject.Interfaces;

namespace FirstProject.Service
{
    public class StorageService : IStorageService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public StorageService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }



        public async Task CreateShop(Shop shop)
        {
            await using var db = new DataContext(_dbContextOptions);
            if (string.IsNullOrWhiteSpace(shop.Name))
                return;

            await db.Shops.AddAsync(shop);
            await db.SaveChangesAsync();
            var test = await db.Sellers.CountAsync();


        }
        public async Task<List<Shop>> GetShops()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.ToListAsync();
            return shops;
        }
        public async Task AddSeller(int shopId, Seller seller)
        {
            if (seller == null)
                return; 
            await using var db = new DataContext(_dbContextOptions);
            var shop = await db.Shops.Where(x => x.Id == shopId).FirstOrDefaultAsync();
            if (shop == null)
                return; 
            if (string.IsNullOrWhiteSpace(seller.Name))
                return; 
            if (shopId == 0)
                return; 
            seller.ShopId = shopId;
            await db.Sellers.AddAsync(seller);
            await db.SaveChangesAsync();
            return;

        }
        public async Task<List<Seller>> GetSellers(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var sellers = await db.Sellers.Where(x => x.ShopId == shopId).ToListAsync();
            return sellers;
        }
        public async Task CreateProduct(Product product)
        {
            await using var db = new DataContext(_dbContextOptions);

            if (string.IsNullOrWhiteSpace(product.Name))
                return;
            if (product.Price < 0)
                return;

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return;

        }
        public async Task<List<Product>> GetProduct()
        {
            await using var db = new DataContext(_dbContextOptions);
            var product = await db.Products.ToListAsync();
            return product;
        }



        public async Task DeliverGoods(int shopId, int productId, double count)
        {
            await using var db = new DataContext(_dbContextOptions);
            var shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
            if (shop == null)
                return; 
            var transaction = db.StorageTransactions.AddAsync(new StorageTransaction { ShopId = shopId, ProductId = productId, Count = count, TransactionType = StorageTransactionType.Take });
            await db.SaveChangesAsync();
            return;

        }
        public async Task<List<StorageTransaction>> GetStorageTransaction(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var transaction = await db.StorageTransactions.Where(x => x.ShopId == shopId).ToListAsync();
            return transaction;
        }

        public async Task GetProduct(List<OrderItem> orders, int shopId)
        {
            if (orders.IsNullOrEmpty())
                return;
            if (shopId == 0)
                return;
            await using var db = new DataContext(_dbContextOptions);
            var shop = await db.Shops.FirstOrDefaultAsync(db => db.Id == shopId);
            if (shop == null)
            {
                // var respose =new BaseResponse() { IsSuccess = true,ErrorMessage="Магазина с данным айди не сущестует" }) ;
                return;
            }
            foreach (var item in orders)
            {
                await db.StorageTransactions.AddAsync(new StorageTransaction { ShopId = shopId, ProductId = item.ProductId, Count = item.Count, TransactionType = StorageTransactionType.Ship });
            }
            await db.SaveChangesAsync();
        }
    }
}