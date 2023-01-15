using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        readonly string s_orderItem = "OrderItems";
       
        public int Add(OrderItem OrderAdd)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            OrderAdd.ID = Config.GetOrderItemNumber();
            listOrderItem.Add(OrderAdd);
            Config.SaveNextOrderItemNumber(OrderAdd.ID + 1);
            XmlTools.SaveListToXMLSerializer(listOrderItem, s_orderItem);
            return OrderAdd.ID;
            
        }

        public void Delete(int id)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            int x = listOrderItem.FindIndex(x => x?.ID == id);
            if (x == -1)
                throw new DalIdDoNotExistException(id, " not fouded");
            listOrderItem.RemoveAt(x);
            XmlTools.SaveListToXMLSerializer(listOrderItem, s_orderItem);

        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            if (filter != null)
                return listOrderItem.Where(item => filter(item));
            return listOrderItem.Select(item => item);

        }

        public List<OrderItem?> GetAllOrderItems(int id)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            List<OrderItem?> newList = new List<OrderItem?>();
            for (int i = 0; i < listOrderItem.Count; i++)
            {
                if (listOrderItem[i]?.OrderId == id)
                {
                    newList.Add(listOrderItem[i]);
                }
            }
            return newList;


           
        }

        public OrderItem GetById(int id)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            OrderItem orderIt = new OrderItem();
            orderIt = listOrderItem.Find(x => x?.ID == id) ?? throw new DalIdDoNotExistException(orderIt.ID, "Order");
            return orderIt;
        }

        public OrderItem GetOrderItem(int orderId, int productId)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            OrderItem? orderItem = null;
            for (int i = 0; i < listOrderItem.Count; i++)
            {
                if (listOrderItem[i]?.ProductId == productId && listOrderItem[i]?.OrderId == orderId)
                {
                    orderItem = listOrderItem[i];
                    break;
                }
            }
            if (orderItem == null)
                throw new DalIdDoNotExistException(orderId, " order item do not exist");
            return (OrderItem)orderItem;
        }

        public void Update(OrderItem orderUp)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
            int x = listOrderItem.FindIndex(x => x?.ID == orderUp.ID);
            if (x == -1)
                throw new DalIdDoNotExistException(orderUp.ID, " not fouded");
            listOrderItem[x] = orderUp;
            XmlTools.SaveListToXMLSerializer(listOrderItem, s_orderItem);

        }
    }
}
