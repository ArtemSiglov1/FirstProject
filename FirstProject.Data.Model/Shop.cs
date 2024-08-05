
namespace FirstProject.Data.Models
{
    /// <summary>
    /// Сущность описывающая магазин
    /// </summary>
    public class Shop
    {
        public int Id { get; set; }//
        public string Name { get; set; }
        public List<Seller> Sellers { get; set; }
        public List<StorageTransaction> Transactions { get; set; }
    }
}