

using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem OrderAdd)
    {
        OrderAdd.ID = DataSource.config.NextOrderItemNumber;
        DataSource.OrderItemList.Add(OrderAdd);
        return OrderAdd.ID;

    }
    public OrderItem GetById(int id)
    {
        OrderItem orderIt = new OrderItem();
        orderIt = DataSource.OrderItemList.Find(x => x?.ID == id) ?? throw new Exception("ID not founed");
        return orderIt;
    }
    public void Update(OrderItem orderUp)
    {

        int x = DataSource.OrderItemList.FindIndex(x => x?.ID == orderUp.ID);
            if(x==-1)
               throw new Exception(" not fouded");
        DataSource.OrderItemList.Insert(x+1,orderUp);
    }
    public void Delete(int id)
    {
       
       int x = DataSource.OrderItemList.FindIndex(x => x?.ID == id);
        if(x==-1)
              throw new Exception(" not fouded");
        DataSource.OrderItemList.RemoveAt(x);
    }
    public IEnumerable<OrderItem?> GetAll()
    {
        List<OrderItem?> OrderItemList1 = new List<OrderItem?>();
        for (int i = 0; i < DataSource.OrderItemList.Count; i++)
        {
            OrderItemList1.Add(DataSource.OrderItemList[i]);
        }
        return OrderItemList1;
    }
    
    public List<OrderItem?> GetAllOrderItems(int id)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        for (int i = 0; i < DataSource.OrderItemList.Count; i++)
        {
            if (DataSource.OrderItemList[i]?.OrderId == id)
            {
                newList.Add(DataSource.OrderItemList[i]);
            }
        }
        return newList;
    }
    public OrderItem GetOrderItem(int orderId, int productId)
    {
        OrderItem? orderItem = null;
        for (int i = 0; i < DataSource.OrderItemList.Count; i++)
        {
            if (DataSource.OrderItemList[i]?.ProductId == productId && DataSource.OrderItemList[i]?.OrderId == orderId)
            {
                orderItem = DataSource.OrderItemList[i];
            }
        }
        if (orderItem == null)

            throw new Exception(" order item do not exist");
        return (OrderItem)orderItem;




































































































































































    }
}
