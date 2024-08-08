using FirstProject.Data;
using FirstProject.Data.Enums;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    public class SellerService:ISellerService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public SellerService()
        {
        }

        public SellerService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<List<OrderItem>> CheckProduct(Order order, int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var productIds = order.Items.Select(x => x.ProductId).ToArray();


            var productCount = await db.StorageTransactions
                 .Where(x => x.ShopId == shopId && productIds.Contains(x.ProductId))
                 .GroupBy(x => x.ProductId)
                 .Select(g => new
                 {
                     ProductId = g.Key,
                     Count = g.Sum(x => x.TransactionType == StorageTransactionType.Take ? x.Count : -x.Count)
                 })
                 .ToArrayAsync();
            //todo: по каждому продукту проверить доступное кол во на складе 
            foreach (var item in order.Items)
            {
                var availableProduct = productCount.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (availableProduct == null || availableProduct.Count < item.Count)
                {
                   return new List<OrderItem> { new OrderItem { ProductId = item.ProductId } }                     ;
                }
            }
            return new List<OrderItem> { };
        }

        public async Task<bool> ProcessOrder(Order order, int shopId, int sellerId)
        {

            await using var db = new DataContext(_dbContextOptions);
           var product=await  CheckProduct(order, shopId);
            if (!product.IsNullOrEmpty())
            {
                return false;
            }
            // Если все товары доступны, списываем их со склада и фиксируем продажу
            foreach (var item in order.Items)
            {
                await db.StorageTransactions.AddAsync(new StorageTransaction
                {
                    ShopId = shopId,
                    ProductId = item.ProductId,
                    Count = item.Count, // Списание товара
                    TransactionType = StorageTransactionType.Ship,
                    DateCreate = DateTime.UtcNow
                });

                await db.OrderTransactions.AddAsync(new OrderTransaction
                {
                    Cost = (decimal)(item.Count * item.Product.Price),
                    DateCreate = DateTime.UtcNow,
                    OrderId = item.OrderId,
                });
            }
            return true;
        }

        //public async Task<BaseResponse> Sale(Order order)
        //{
        //    if (order == null) 
        //       return new BaseResponse() { IsSuccess = true ,ErrorMessage="Вы не указали заказ"};
        //    await using var db = new DataContext(_dbContextOptions);
        //    var seller = await db.Sellers.FirstOrDefaultAsync(x => x.Id == order.SellerId);
        //    if (seller == null)
        //        return new BaseResponse { IsSuccess = true, ErrorMessage = "Продавец с данным айди не существует" };
        //    var shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == seller.ShopId);
        //    if (shop == null)
        //        return new BaseResponse() { IsSuccess = true, ErrorMessage = "Магазина с данным айди не существует " };
        //    var productIds = order.Items.Select(x => x.ProductId).ToArray();
        //    var productCount = await db.StorageTransactions
        //         .Where(x => x.ShopId == shop.Id && productIds.Contains(x.ProductId))
        //         .GroupBy(x => x.ProductId)
        //         .Select(g => new
        //         {
        //             ProductId = g.Key,
        //             Count = g.Sum(x => x.TransactionType == StorageTransactionType.Take ? x.Count : -x.Count)
        //         })
        //         .ToArrayAsync();
        //    //todo: по каждому продукту проверить доступное кол во на складе 
        //    foreach (var item in order.Items)
        //    {
        //        var availableProduct = productCount.FirstOrDefault(p => p.ProductId == item.ProductId);
        //        if (availableProduct == null || availableProduct.Count < item.Count)
        //        {
        //            return new BaseResponse
        //            {
        //                IsSuccess = false,
        //                ErrorMessage = $"Недостаточно товара с ID {item.ProductId} на складе"
        //            };
        //        }
        //    }

        //    // Если все товары доступны, списываем их со склада и фиксируем продажу
        //    foreach (var item in order.Items)
        //    {
        //      await  db.StorageTransactions.AddAsync(new StorageTransaction
        //        {
        //            ShopId = shop.Id,
        //            ProductId = item.ProductId,
        //            Count = item.Count, // Списание товара
        //            TransactionType = StorageTransactionType.Ship,
        //            DateCreate = DateTime.UtcNow
        //        });

        //     await   db.OrderTransactions.AddAsync(new OrderTransaction
        //        {
        //            Cost = (decimal)(item.Count*item.Product.Price),
        //            DateCreate = DateTime.UtcNow,
        //            OrderId=item.OrderId,
        //        });
        //    }

        //    await db.SaveChangesAsync();

        //    return new BaseResponse { IsSuccess = true, ErrorMessage = "Продажа успешно завершена" };
       
        //}
        
    }
}
