using DO;
namespace Dal;

public class DalOrder
{
    

    public int Add ( Order Order )
    {
        Order.ID = DataSource.config.nextOrderNumber;
        DataSource.orderList.Add(Order);
        return Order.ID;
       
    }


    public Order GetById(int id)
    {
        Order order = new Order();
        order = DataSource.orderList.Find(x => x?.ID == id) ?? throw new Exception("ID not founed");
            return order;   

    }


    public void Update (Order order)
    {


        int x = DataSource.orderList.FindIndex(x => x?.ID == order.ID);
        if(x == -1)
             throw new Exception(" not fouded");
        DataSource.orderList.Insert(x+1,order);
    }
    public void Delete (int id)
    {

        int x = DataSource.orderList.FindIndex(x => x?.ID == id);
        if( x == -1)
              throw new Exception(" not fouded");
        DataSource.orderList.RemoveAt(x);
    }
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> orderList1 = new List<Order?>();
        for (int i = 0; i < DataSource.orderList.Count; i++)
        {
            Order? order1 = new Order();
            order1 = DataSource.orderList[i];
            orderList1.Add(order1);
        }
        return orderList1;
    }



}
