using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using FirstProject.Data.Enums;
using Microsoft.IdentityModel.Tokens;

namespace FirstProject.Service
{
    public class StorageService
    {
       


        
            private DbContextOptions<DataContext> _dbContextOptions;

            public StorageService(DbContextOptions<DataContext> dbContextOptions)
            {
                _dbContextOptions = dbContextOptions;
            }
            public async Task<BaseResponse> CreateShop(Shop shop)
            {
                await using var db = new DataContext(_dbContextOptions);
                if (string.IsNullOrWhiteSpace(shop.Name))
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы не указали имя" };

                await db.Shops.AddAsync(shop);
                await db.SaveChangesAsync();
                return new BaseResponse() { IsSuccess = true };

            }
        public async Task<List<Shop>> GetShops()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.ToListAsync();
            return shops;
        }
            public async Task<BaseResponse> AddSeller(int shopId, Seller seller)
            {
            if (seller == null)
                return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы не ввели данные о продавце" };
                await using var db = new DataContext(_dbContextOptions);
                var shop = await db.Shops.Where(x => x.Id == shopId).FirstOrDefaultAsync();
                if (shop == null)
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы указали неверный айди магазина" };
                if (string.IsNullOrWhiteSpace(seller.Name))
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы не указали имя" };
                if (shopId == 0)
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы не указали айди магазина" };
                seller.ShopId = shopId;
                await db.Sellers.AddAsync(seller);
                await db.SaveChangesAsync();
                return new BaseResponse() { IsSuccess = true };

            }
        public async Task<List<Seller>> GetSellers(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var sellers=await db.Sellers.Where(x=>x.ShopId==shopId).ToListAsync();
            return sellers;
        }
        public async Task<BaseResponse> CreateProduct(Product product)
            {
                await using var db = new DataContext(_dbContextOptions);

                if (string.IsNullOrWhiteSpace(product.Name))
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Вы не указали имя" };
                if (product.Price < 0 )
                    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Цена не попадает в заданный диапазон" };

                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return new BaseResponse() { IsSuccess = true };

            }
        public async Task<List<Product>> GetProduct()
        {
            await using var db = new DataContext(_dbContextOptions);
            var product = await db.Products.ToListAsync();
            return product;
        }



        public async Task<BaseResponse> DeliverGoods(int shopId,int productId,double count)
        {
            await using var db=new DataContext(_dbContextOptions);
            var shop = await db.Shops.FirstOrDefaultAsync(x=>x.Id==shopId);
            if (shop == null)
                return new BaseResponse() { IsSuccess = true, ErrorMessage = "Магазина с таким айди не существет" };
            var transaction = db.StorageTransactions.AddAsync(new StorageTransaction {ShopId=shopId ,ProductId=productId,Count=count, TransactionType=StorageTransactionType.Take});
            await db.SaveChangesAsync();
            return new BaseResponse() { IsSuccess = true};
            
        }
        public async Task<List<StorageTransaction>> GetStorageTransaction(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var transaction = await db.StorageTransactions.Where(x=>x.ShopId==shopId).ToListAsync();
            return transaction;
        }
    }
}