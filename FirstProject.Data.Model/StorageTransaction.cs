using FirstProject.Data.Enums;
namespace FirstProject.Data.Models
{
    public class StorageTransaction
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public double Count { get; set; }
        public DateTime DateCreate { get; set; }
        public StorageTransactionType TransactionType { get; set; }
    }
}
