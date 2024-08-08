using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Models.BussinesLogic;

namespace FirstProject.Service
{
    /// <summary>
    /// класс для работы с консолью 
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="storageService">интерфейс сервиса склада</param>
        /// <param name="clientService">интерфейс сервиса клиента</param>
        /// <param name="dataService">интерфейс сервиса бд</param>
        /// <param name="sellerService">интерфейс сервиса продавца</param>
        public Manager(IStorageService storageService, IClientService clientService, IDataModulService dataService, ISellerService sellerService)
        {
            StorageService = storageService;
            ClientService = clientService;
            DataService = dataService;
            SellerService = sellerService;

        }
        /// <summary>
        /// интерфейс сервиса склада
        /// </summary>
        public IStorageService StorageService { get; set; }
        /// <summary>
        /// интерфейс сервиса клиента
        /// </summary>
        public IClientService ClientService { get; set; }
        /// <summary>
        /// интерфейс сервиса бд
        /// </summary>
        public IDataModulService DataService { get; set; }
        /// <summary>
        /// интерфейс сервиса продавца
        /// </summary>
        public ISellerService SellerService { get; set; }
        /// <summary>
        /// статистика заказов 
        /// </summary>
        public Statistics Stat { get; set; }
        /// <summary>
        /// проводник 
        /// </summary>
        public Provider Provider { get; set; }
        /// <summary>
        /// список клиентов
        /// </summary>
        public List<Client> Clients { get; set; }
        /// <summary>
        /// список магазинов
        /// </summary>
        public List<Magazin> Magazins { get; set; }
        /// <summary>
        /// метод работающий по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void Stat_OnCancel(object sender, int e)
        {
            // Stat.OnCancel?.Invoke(this, e);
        }
        /// <summary>
        /// запуск таймера
        /// </summary>
        public void Start()
        {
            foreach (var client in Clients)
            {
                client.OnBuy += Client_OnBuy;
                client.StartTimer(1000);
            }
        }
        /// <summary>
        /// добавление клиента
        /// </summary>
        /// <param name="count"></param>
        public void InitClient(int count)
        {
            Clients = new List<Client>();
            for (int i = 0; i < count; i++)
                Clients.Add(new Client(ClientService, DataService) {ClientService=ClientService,DataModulService=DataService, BuyerId = DataService.GetRandomBuyer().Id });
        }
        /// <summary>
        /// клиент купил ил нет 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Client_OnBuy(object sender, Order e)
        {
            //todo найти магазин по айди обработать заказ 
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
