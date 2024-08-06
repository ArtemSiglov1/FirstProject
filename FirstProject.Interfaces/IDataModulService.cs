using FirstProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FirstProject.BussinesLogic;

namespace FirstProject.Interfaces
{
    public interface IDataModulService
    {
        // public List<BusinessSeller> GetSellers();
        public Task<Buyer> GetRandomBuyer();
        public Task<Seller> GetRandomSeller();
        public Task<Product> GetRandomProduct();
    }
}
