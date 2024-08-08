using FirstProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Interfaces
{
    public interface ISellerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shopId"></param>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        public Task<bool> ProcessOrder(Order order,int shopId,int sellerId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public Task<List<OrderItem>> CheckProduct(Order order,int shopId);
    }
}
