using DO;
namespace Dal;

public class DalOrder
{
    

    public int Add ( Order Order )
    {
        Order.ID = DataSource.config.NextOrderNumber;
        DataSource.OrderList.Add(Order);
        return Order.ID;
       
    }
    public Order GetById(int id)
    {
        Order order = new Order();
        order = DataSource.OrderList.Find(x => x?.ID == id) ?? throw new Exception("ID not founed");
            return order;   

    }
    public void Update (Order order)
    {


        int x = DataSource.OrderList.FindIndex(x => x?.ID == order.ID);
        if(x == -1)
             throw new Exception(" not fouded");
        DataSource.OrderList.Insert(x+1,order);
    }
    public void Delete (int id)
    {

        int x = DataSource.OrderList.FindIndex(x => x?.ID == id);
        if( x == -1)
              throw new Exception(" not fouded");
        DataSource.OrderList.RemoveAt(x);
    }
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> OrderList1 = new List<Order?>();
        for (int i = 0; i < DataSource.OrderList.Count; i++)
        {
            Order? order1 = new Order();
            order1 = DataSource.OrderList[i];
            OrderList1.Add(order1);
        }
        return OrderList1;
    }



}
