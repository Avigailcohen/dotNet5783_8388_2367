using DalApi;
using DO;
namespace Dal;

internal class DalOrder : IOrder
{


    public int Add(Order Order)
    {
        Order.ID = DataSource.config.nextOrderNumber;
        DataSource.orderList.Add(Order);
        return Order.ID;

    }


    public Order GetById(int id)
    {
        Order order = new Order();
        order = DataSource.orderList.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException( order.ID,"ID ");
        return order;

    }


    public void Update(Order order)
    {
        int x = DataSource.orderList.FindIndex(x => x?.ID == order.ID);
        if (x == -1)
            throw new DalIdDoNotExistException(order.ID," Order");
        DataSource.orderList[x] = order;
    }
    public void Delete(int id)
    {

        int x = DataSource.orderList.FindIndex(x => x?.ID == id);
        if (x == -1)
            throw new DalIdDoNotExistException(id," not fouded");
        DataSource.orderList.RemoveAt(x);
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.orderList.Where(item => filter(item));
        return DataSource.orderList.Select(item => item);
    }



}
