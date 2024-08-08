using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    public class ClientService:IClientService
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        public ClientService()
        {
        }

        public ClientService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
       public List<Product> Products { get; set; }
        public async Task<Order> CreateOrders(int buyerId,int sellerId,List<OrderItem> items)
        {
            await using var db = new DataContext(_dbContextOptions);
            var buyer = await db.Buyers.AnyAsync(x => x.Id == buyerId);
            if(!buyer)
            {
                Console.WriteLine(new BaseResponse() { IsSuccess=true,ErrorMessage="покупатель с данным не найден"});return null;
            }
            var seller=await db.Sellers.AnyAsync(db => db.Id == sellerId);
            if(!seller)
            {
                Console.WriteLine(new BaseResponse() { IsSuccess = true, ErrorMessage = "продовец с данным айди не найден" }) ;return null;
            }
            var order = new Order { BuyerId = buyerId, SellerId = sellerId, Items = items };
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return order;
        }
    }
}
