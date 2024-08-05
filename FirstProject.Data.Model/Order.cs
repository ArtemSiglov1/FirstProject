using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public List<OrderItem> Items { get; set; }
        public int BuyerId { get;set; }
        public Buyer Buyer { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public OrderTransaction OrderTransaction { get; set; }
    }
}
