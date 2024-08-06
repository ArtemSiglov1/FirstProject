using FirstProject.Data.Models;
using FirstProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Models.BussinesLogic
{
    public class BusinessSeller
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public SellerService Service { get; set; }
        //public List<OrderItem> ProcessOrder(Order order)
        //{

        //}
    }
}
