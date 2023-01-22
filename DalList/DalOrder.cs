using DalApi;
using DO;
namespace Dal;

internal class DalOrder : IOrder
{

    /// <summary>
    /// add order to the list of the order(in Data source)
    /// </summary>
    /// <param name="Order"></param>
    /// <returns></returns>
    public int Add(Order Order)
    {
        Order.ID = DataSource.config.nextOrderNumber;
        DataSource.orderList.Add(Order);
        return Order.ID;

    }

    /// <summary>
    /// function which check if the order exist and retun by his ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalIdDoNotExistException"></exception>
    public Order GetById(int id)
    {
        Order order = new Order();
        order = DataSource.orderList.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException( order.ID,"ID ");
        return order;

    }

    /// <summary>
    /// function which update order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="DalIdDoNotExistException"></exception>
    public void Update(Order order)
    {
        int x = DataSource.orderList.FindIndex(x => x?.ID == order.ID);
        if (x == -1)
            throw new DalIdDoNotExistException(order.ID," Order");
        DataSource.orderList[x] = order;
    }
    /// <summary>
    /// function which delete  order from the list of the orders
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalIdDoNotExistException"></exception>
    public void Delete(int id)
    {

        int x = DataSource.orderList.FindIndex(x => x?.ID == id);
        if (x == -1)
            throw new DalIdDoNotExistException(id," not fouded");
        DataSource.orderList.RemoveAt(x);
    }
    /// <summary>
    /// function which return IEnumerable of orders,she can return the list by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.orderList.Where(item => filter(item));
        return DataSource.orderList.Select(item => item);
    }



}
