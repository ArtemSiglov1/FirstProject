using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public List<Order> Orders { get; set; }
    }
}
