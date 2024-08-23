using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Enums
{
    /// <summary>
    /// тип транзакции
    /// </summary>
    public enum StorageTransactionType
    {
        /// <summary>
        /// привезли 
        /// </summary>
        Take=1,
        /// <summary>
        /// отвезли
        /// </summary>
        Ship=2,
    }
}
