using FirstProject.Data.Models;
namespace FirstProject.Interfaces
{
  public interface IClientService {
       public  Task<Order> CreateOrders(int buyerId, int sellerId, List<OrderItem> items);
  }
}