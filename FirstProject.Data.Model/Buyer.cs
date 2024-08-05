using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set;}
    }
}
