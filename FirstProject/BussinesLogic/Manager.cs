//using FirstProject.Data.Models;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Models.BussinesLogic;

namespace FirstProject.Service
{
    public class Manager
    {
        public Manager(IStorageService storageService, IClientService clientService, IDataModulService dataService, ISellerService sellerService)
        {
            StorageService = storageService;
            ClientService = clientService;
            DataService = dataService;
            SellerService = sellerService;

        }

        public IStorageService StorageService { get; set; }
        public IClientService ClientService { get; set; }
        public IDataModulService DataService { get; set; }
        public ISellerService SellerService { get; set; }
        public Statistics Stat { get; set; }
        public Provider Provider { get; set; }
        public List<Client> Clients { get; set; }
        public List<Magazin> Magazins { get; set; }

        public void Stat_OnCancel(object sender, int e)
        {
            // Stat.OnCancel?.Invoke(this, e);
        }
        public void Start()
        {
            foreach (var client in Clients)
            {
                client.StartTimer(1000);
            }
        }
        public void InitClient(int count)
        {
            Clients = new List<Client>();
            for (int i = 0; i < count; i++)
                Clients.Add(new Client(ClientService, DataService) {ClientService=ClientService,DataModulService=DataService, BuyerId = DataService.GetRandomBuyer().Id });
        }
        public void Client_OnBuy(object sender, Order e)
        {

        }
        //public List<Magazin> InitShop()
        //{

        //}
        //public async Task<BaseResponse> Shop_OrderProduct(List<OrderItem> list, int id)
        //{
        //    return await Provider.GetProduct(list, id);
        //}
    }
}
