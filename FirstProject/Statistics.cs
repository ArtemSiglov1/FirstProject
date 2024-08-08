using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    public class Statistics
    {
        public double PassedOrders { get;set; }
        public double ThereAreOrders { get;set; }
        private System.Timers.Timer _timer;
        public double CountOrder { get;set; }
        public event EventHandler<int> OnCancel;
        public void Timer_Elapsed(object sender, EventArgs e)
        {
            Console.WriteLine(PassedOrders);
        }
        public void Start()
        {
            _timer = new System.Timers.Timer(1000);

            // Подписка на событие Elapsed, которое срабатывает при истечении интервала
            _timer.Elapsed += Timer_Elapsed;

            // Автоматический перезапуск таймера
            _timer.AutoReset = true;
            _timer.Start();
        }
        public void ChangePassed(object sender, EventArgs e)
        {

        }
    }
}
