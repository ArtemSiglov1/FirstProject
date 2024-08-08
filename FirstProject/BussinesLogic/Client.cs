using FirstProject.Data.Models;
using FirstProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FirstProject.Models.BussinesLogic
{
    public class Client
    {
        public int BuyerId { get; set; }
        public Client(IClientService clientService, IDataModulService dataModul)
        {
            ClientService = clientService;
            DataModulService= dataModul;
        }

        public IClientService ClientService { get; set; }
        public IDataModulService DataModulService { get; set; }
        private System.Timers.Timer _timer;

        public void StartTimer(double intervalInMilliseconds)
        {
            _timer = new System.Timers.Timer(intervalInMilliseconds);

            // Подписка на событие Elapsed, которое срабатывает при истечении интервала
            _timer.Elapsed += OnTimedEvent;

            // Автоматический перезапуск таймера
            _timer.AutoReset = true;
            _timer.Start();


        }
        private async Task<List<OrderItem>> GenerateRandomOrder()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            Random random = new Random();
            int n = random.Next(1, 25);
            for (int i = 0; i < n; i++)
            {
                var product =await DataModulService.GetRandomProduct();
                var randomCount=random.Next(1, 25);
                    var cost = product.Price * randomCount;

                orderItems.Add(new OrderItem() { ProductId = product.Id, Count = randomCount, Cost = (decimal)cost });
            }
            return orderItems;
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var buyer = DataModulService.GetRandomBuyer();
            var seller = DataModulService.GetRandomSeller();
            var orderDetails = await GenerateRandomOrder();
            var order =await ClientService.CreateOrders(buyer.Id, seller.Id, orderDetails);
            Console.WriteLine("/DDD/");
            if (order != null)
            {
                Console.WriteLine("/DDD");
                OnBuy?.Invoke(this, order);
                
            }
            else
            {
                await Console.Out.WriteLineAsync("Try Again");
            }
        }

        public void StopTimer()
        {
            // Остановка таймера
            _timer.Stop();
            _timer.Dispose();
        }
        public event EventHandler<Order> OnBuy;


    }

}

