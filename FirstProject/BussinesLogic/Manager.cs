using FirstProject.Interfaces;
using FirstProject.Models.BussinesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    public class Manager
    {
        public Manager(IStorageService storageService, IClientService clientService, IDataModulService dataService, ISellerService sellerService, Statistic stat, Provider provider, List<Client> clients, List<Shop> shops)
        {
            StorageService = storageService;
            ClientService = clientService;
            DataService = dataService;
            SellerService = sellerService;
            Stat = stat;
            Provider = provider;
            Clients = clients;
            Shops = shops;
        }

        public IStorageService StorageService { get; set; }
        public IClientService ClientService { get; set; }
        public IDataModulService DataService { get; set; }
        public ISellerService SellerService { get; set; }
        public Statistic Stat { get; set; }
        public Provider Provider { get; set; }
        public List<Client> Clients { get; set; }
        public List<Shop> Shops { get; set; }



    }
}
