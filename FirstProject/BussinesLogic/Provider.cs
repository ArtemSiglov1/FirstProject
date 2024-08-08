using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Models.BussinesLogic
{
    public class Provider
    {

        private DbContextOptions<DataContext> _dbContextOptions;

        public Provider()
        {
        }

        public Provider(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public Provider(IStorageService service)
        {
            Service = service;
        }

        public IStorageService Service { get; set; }
        public async Task<BaseResponse> GetProduct(List<OrderItem> orders,int shopId)
        {
            if (orders == null)
                return new BaseResponse { IsSuccess = true, ErrorMessage = "Вы не указали список заказов" };
            await using var db = new DataContext(_dbContextOptions);
            var shop=await db.Shops.Where(x => x.Id == shopId).FirstOrDefaultAsync();
            if (shop== null)
                return new BaseResponse() { IsSuccess = true, ErrorMessage = "Магазина с данным айди не существует" };
            foreach (var order in orders)
            {
                await db.StorageTransactions.AddAsync(new StorageTransaction()
                {
                    ShopId = shopId,
                    ProductId = order.ProductId,
                    Count = order.Count,
                    DateCreate = DateTime.Now,
                    TransactionType = Data.Enums.StorageTransactionType.Take
                });
            }
            await db.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

    }
}
