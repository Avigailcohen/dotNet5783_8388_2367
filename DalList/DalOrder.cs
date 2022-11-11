using DO;
namespace Dal;

public class DalOrder
{
    public void Add ( Order Order )
    {
      DataSource.OrderList.Exists(x=>x.ID);   
    }
    public int Update { get; set; } 
}
