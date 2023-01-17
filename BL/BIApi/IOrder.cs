using BO;

namespace BIApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetOrders();
    BO.Order GetOrderById(int OrderId);
    BO.Order UpdateOrderShip(int OrderId);
    BO.Order UpdateDelivertOrder(int OrderId);
    BO.OrderTracking OrderTracking(int OrderId);
    IEnumerable<IGrouping<double, OrderForList>> GetGroupedOrderes(bool ascending = true);
    BO.Order UpdateOrder(int orderId, int productId, int newAmount);


}
