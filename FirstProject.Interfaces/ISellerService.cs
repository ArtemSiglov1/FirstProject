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
        public Task<bool> ProcessOrder(Order order,int shopId,int sellerId);
        public Task<List<OrderItem>> CheckProduct(Order order,int shopId);
    }
}
