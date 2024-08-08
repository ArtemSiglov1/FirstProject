using FirstProject.Data.Models;
using FirstProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Models.BussinesLogic
{
    /// <summary>
    /// бизнес модель магазина
    /// </summary>
    public class Magazin
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// лист бизнес моделей продавцов
        /// </summary>
        public List<BusinessSeller> Sellers { get; set; }
        /// <summary>
        /// конструктор 
        /// </summary>
        /// <param name="dataService">интерфейс сервиса бд</param>
        /// <param name="shopId">идентиф магаза</param>
        /// <param name="sellerService">интерфейс сервиса продавца</param>
        public Magazin(IDataModulService dataService, int shopId,ISellerService sellerService)
        {
            Id = shopId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task ProcessOrder(Order order)
        {

            if (order == null) { }
        }

        /// <summary>
        /// меняет кол во пришедших заказов
        /// </summary>
       public  event EventHandler<int> OnPassedOrders;
        /// <summary>
        /// меняет число выполненых заказов
        /// </summary>
        public event EventHandler<int> OnThereAreOrders;
    }
}
