using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalOrder : IOrder
    {
        readonly string s_order = "orders";
        public int Add(Order Order)
        {
            List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            Order.ID=Config.GetNextOrderNumber();
            listOrders.Add(Order);
            Config.SaveNextOrderNumber(Order.ID+1);
            XmlTools.SaveListToXMLSerializer(listOrders, "orders");
            return Order.ID;
        }

        public void Delete(int id)
        {
            List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            int x = listOrders.FindIndex(x => x?.ID == id);
            if (x == -1)
                throw new DalIdDoNotExistException(id, " not fouded");
            listOrders.RemoveAt(x);
            XmlTools.SaveListToXMLSerializer(listOrders, s_order);
        }

        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            if (filter != null)
                return listOrders.Where(item => filter(item));
            return listOrders.Select(item => item);
            
        }

        public Order GetById(int id)
        {
            List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            Order order = new Order();
            order = listOrders.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException(order.ID, "ID ");
            return order;
        }

        public void Update(Order order)
        {
            List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_order);
            int x = listOrders.FindIndex(x => x?.ID == order.ID);
            if (x == -1)
                throw new DalIdDoNotExistException(order.ID, " Order");
            listOrders[x] = order;
            XmlTools.SaveListToXMLSerializer(listOrders, s_order);

        }
    }
}
