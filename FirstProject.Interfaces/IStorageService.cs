using FirstProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstProject.Interfaces
{
    public interface IStorageService
    {
        public Task GetProduct(List<OrderItem> orders,int shopId);
        public Task CreateShop(Shop shop);
        public Task AddSeller(int shopId, Seller seller);
        public Task CreateProduct(Product product);
        public Task DeliverGoods(int shopId, int productId, double count);


    }
}
