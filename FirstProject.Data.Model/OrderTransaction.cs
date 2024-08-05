using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    public class OrderTransaction
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal Cost { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
