using DO;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        List<OrderItem?> GetAllOrderItems(int id);
        OrderItem GetOrderItem(int orderId, int productId);
    }
}
