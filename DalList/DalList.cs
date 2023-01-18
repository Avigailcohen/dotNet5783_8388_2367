using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public  static IDal Instance { get; } = new DalList();
        private DalList() { }
        
        public  IOrder Order { get; } = new DalOrder();
        public IProduct Product { get; }= new DalProduct();
        public IOrderItem OrderItem { get; }= new DalOrderItem();
        public IUser User { get; }=new DalUser();

    }
}
