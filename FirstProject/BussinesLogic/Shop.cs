using FirstProject.Data.Models;
using FirstProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Models.BussinesLogic
{
    public class Shop
    {
        public int Id { get; set; }
        public List<BusinessSeller> Sellers { get; set; }
        public Shop(IDataModulService dataService, int shopId,ISellerService sellerService)
        {
            Id = shopId;
        }
        public async Task ProcessOrder(Order order)
        {

            if (order == null) { }
        }
       public  event EventHandler<int> OnPassedOrders;
        public event EventHandler<int> OnThereAreOrders;
    }
}
