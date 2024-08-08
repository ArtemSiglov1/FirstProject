using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    /// <summary>
    /// статистика
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// всего заказов
        /// </summary>
        public double PassedOrders { get;set; }
        /// <summary>
        /// заказов записанных в бд
        /// </summary>
        public double ThereAreOrders { get;set; }

        /// <summary>
        /// таймер для вывода количества сделанных заказов
        /// </summary>
        private System.Timers.Timer _timer;
        /// <summary>
        /// количество заказов для отсановки
        /// </summary>
        public double CountOrder { get;set; }
        /// <summary>
        /// событие для остановки клиентов
        /// </summary>
        public event EventHandler<int> OnCancel;
        /// <summary>
        /// количество заказов в консоль 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Elapsed(object sender, EventArgs e)
        {
            Console.WriteLine(PassedOrders);
        }
        /// <summary>
        /// включает таймер
        /// </summary>
        public void Start()
        {
            _timer = new System.Timers.Timer(1000);

            // Подписка на событие Elapsed, которое срабатывает при истечении интервала
            _timer.Elapsed += Timer_Elapsed;

            // Автоматический перезапуск таймера
            _timer.AutoReset = true;
            _timer.Start();
        }
        /// <summary>
        /// меняет кол-во сделанных заказов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangePassed(object sender, EventArgs e)
        {

        }
    }
}
