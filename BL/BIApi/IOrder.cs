using BO;

namespace BIApi;

public interface IOrder
{
    /// <summary>
    /// return IEnumerable of order for list 
    /// </summary>
    /// <returns></returns>
    IEnumerable<OrderForList> GetOrders();
    /// <summary>
    /// return order by her id
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    BO.Order GetOrderById(int OrderId);
    /// <summary>
    /// update thr ship date
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    BO.Order UpdateOrderShip(int OrderId);
    /// <summary>
    /// update delivery date
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    BO.Order UpdateDelivertOrder(int OrderId);
    /// <summary>
    /// function which return information about the status of the order.
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    BO.OrderTracking OrderTracking(int OrderId);
    /// <summary>
    /// function which grouping (לבדוק איך לשלב בpl)
    /// </summary>
    /// <param name="ascending"></param>
    /// <returns></returns>
    IEnumerable<IGrouping<double, OrderForList>> GetGroupedOrderes(bool ascending = true);
    /// <summary>
    /// (BONUS) funcion which can update the amount (by the manager)
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    BO.Order UpdateOrder(int orderId, int productId, int newAmount);


}
