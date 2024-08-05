using FirstProject.Data;
using FirstProject.Data.Enums;
using FirstProject.Data.Models;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    public class SellerService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public SellerService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<bool> CheckAvailableProducts(/*List<OrderItem> orderItems,*/int shopId,Order order)
        {
            await using var db = new DataContext(_dbContextOptions);

            
            //return checks;
        }
        public async Task<BaseResponse> Sale(Order order)
        {
            if (order == null) 
               return new BaseResponse() { IsSuccess = true ,ErrorMessage="Вы не указали заказ"};
            await using var db = new DataContext(_dbContextOptions);
            //if (buyer == false)
            //    return new BaseResponse() { IsSuccess = true, ErrorMessage = "Покупателя с таким айди не существет" };
            var seller = await db.Sellers.FirstOrDefaultAsync(x => x.Id == order.SellerId);
            ////if (seller == null)
            ////    return new BaseResponse { IsSuccess = true, ErrorMessage = "Продавец с данным айди не существует" };
            var shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == seller.ShopId);
            ////if (shop == null)
            ////    return new BaseResponse() { IsSuccess = true, ErrorMessage = "" };
            var productIds = order.Items.Select(x => x.ProductId).ToArray();
            var productCount = await db.StorageTransactions
                 .Where(x => x.ShopId == shop.Id && productIds.Contains(x.ProductId))
                 .GroupBy(x => x.ProductId)
                 .Select(g => new
                 {
                     ProductId = g.Key,
                     Count = g.Sum(x => x.TransactionType == StorageTransactionType.Take ? x.Count : -x.Count)
                 })
                 .ToArrayAsync();
            //todo по каждому продукту проверить доступное кол во на складе 
            var check = order.Items.Select(x => productCount.Any(w => w.ProductId == x.ProductId) && productCount.Any(y => y.Count > x.Count)==true?x.Cost*(decimal)x.Count:0).ToArray();
            
        }
        
    }
}
