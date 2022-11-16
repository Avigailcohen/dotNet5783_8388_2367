

using DO;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Add orderItem by using in config
    /// </summary>
    /// <param name="OrderAdd"></param>
    /// <returns>the OrderID</returns>
    public int Add(OrderItem OrderAdd)
    {
        OrderAdd.ID = DataSource.config.nextOrderItemNumber;
        DataSource.orderItemList.Add(OrderAdd);
        return OrderAdd.ID;

    }
    /// <summary>
    /// get the object by his ID. and check if he exist
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OrderItem</returns>
    /// <exception cref="Exception"></exception>

    public OrderItem GetById(int id)
    {
        OrderItem orderIt = new OrderItem();
        orderIt = DataSource.orderItemList.Find(x => x?.ID == id) ?? throw new Exception("ID not founed");
        return orderIt;
    }
    /// <summary>
    /// update OrderItem by find the Index, if he is not exist throw else insert
    /// </summary>
    /// <param name="orderUp"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem orderUp)
    {

        int x = DataSource.orderItemList.FindIndex(x => x?.ID == orderUp.ID);
            if(x==-1)
               throw new Exception(" not fouded");
        DataSource.orderItemList.Insert(x+1,orderUp);
    }
    /// <summary>
    /// delete orderItem by his ID, check if exist
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
       
       int x = DataSource.orderItemList.FindIndex(x => x?.ID == id);
        if(x==-1)
              throw new Exception(" not fouded");
        DataSource.orderItemList.RemoveAt(x);
    }
    /// <summary>
    /// create a new list of OrderItem By copying the organs in the list
    /// </summary>
    /// <returns>List of OrderItems</returns>
    public IEnumerable<OrderItem?> GetAll()
    {
        List<OrderItem?> OrderItemList1 = new List<OrderItem?>();
        for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            OrderItemList1.Add(DataSource.orderItemList[i]);
        }
        return OrderItemList1;
    }

    /// <summary>
    /// get ID and copy the list By copying the organs in the list
    /// </summary>
    /// <param name="id"></param>
    /// <returns>newList with the same ID</returns>
    public List<OrderItem?> GetAllOrderItems(int id)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            if (DataSource.orderItemList[i]?.OrderId == id)
            {
                newList.Add(DataSource.orderItemList[i]);
            }
        }
        return newList;
    }
    /// <summary>
    /// check OrderItem by OrderID and ProductID and copying the organs in the list
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns>OrderItem</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItem(int orderId, int productId)
    {
        OrderItem? orderItem = null;
        for (int i = 0; i < DataSource.orderItemList.Count; i++)
        {
            if (DataSource.orderItemList[i]?.ProductId == productId && DataSource.orderItemList[i]?.OrderId == orderId)
            {
                orderItem = DataSource.orderItemList[i];
            }
        }
        if (orderItem == null)

            throw new Exception(" order item do not exist");
        return (OrderItem)orderItem;




































































































































































    }
}
