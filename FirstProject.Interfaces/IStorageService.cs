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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public Task GetProduct(List<OrderItem> orders,int shopId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public Task CreateShop(Shop shop);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<Shop>> GetShops();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="seller"></param>
        /// <returns></returns>
        public Task AddSeller(int shopId, Seller seller);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Task CreateProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyers"></param>
        /// <returns></returns>
        public Task AddBuyer(List<Buyer> buyers);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task DeliverGoods(int shopId, int productId, double count);


    }
}
