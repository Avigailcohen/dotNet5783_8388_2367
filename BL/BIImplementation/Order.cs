using BIApi;
using BO;
///לשאול לגבי התאריכים של ההזמנות איפה לעשות את הtry

namespace BIImplementation
{
    internal class Order : IOrder
    {

        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// return list of orders 
        /// </summary>
        /// <returns>kist of order </returns>
        public IEnumerable<BO.OrderForList> GetOrders()// בקשת רשימת הזמנות למנהל 
        {
            IEnumerable<DO.Order?> orders = dal!.Order.GetAll();
            IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll();

            return from DO.Order item in orders
                   let orderItems = items.Where(items => items!.Value.OrderId == item.ID)
                   select new BO.OrderForList()
                   {
                       ID = item.ID,
                       CustomerName = item.CustomerName,
                       OrderStatus = GetStatus(item),
                       AmountItems = orderItems.Count(),
                       TotalPrice = orderItems.Sum(items => items!.Value.Amount * items.Value.Price)
                   };
        }
        /// <summary>
        /// private function for calculate the status of the order by 3 station (ordered,shipped,deliverd)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        private BO.OrderStatus GetStatus(DO.Order order)
        {
            return order.DeliveryDate != null ? BO.OrderStatus.Delivered : order.ShipDate != null
                ? BO.OrderStatus.Shipped : BO.OrderStatus.Ordered;
        }
        /// <summary>
        /// get the order by his ID
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlInvalidInputException"></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>

        public BO.Order GetOrderById(int OrderId)//בקשת פרטי הזמנה למנהל 
        {

            if (OrderId < 0)
                throw  new BO.BlInvalidInputException("order id");

            DO.Order order = new DO.Order();
              try
            {
                order = dal!.Order.GetById(OrderId);
            }  
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }

            return new BO.Order()///copy from do to bo 
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                Status = GetStatus(order),
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                OrderItems = GetOrderItemList(dal.OrderItem.GetAll().Where(x => x?.OrderId == order.ID)),
                TotalPrice = GetOrderItemList(dal.OrderItem.GetAll().Where(x => x?.OrderId == order.ID)).Sum(x => x!.TotalPrice)
            };
        }
        /// <summary>
        /// private function for get the list of the  order item for each order instead of doing it inside the function above 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        private IEnumerable<BO.OrderItem?> GetOrderItemList(IEnumerable<DO.OrderItem?> list)
        {
            return from DO.OrderItem item in list
                   select new BO.OrderItem()
                   {
                       ID = item.ID,
                       Name = dal!.Product.GetById(item.ProductId).Name,
                       Price = item.Price,
                       AmountOfItem = item.Amount,
                       TotalPrice = item.Price * item.Amount,
                       ProductID = dal.Product.GetById(item.ProductId).ID
                       
                   };
        }

        /// <summary>
        /// update the ship date 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>the updated date </returns>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.Order UpdateOrderShip(int OrderId)//עדכון שילוח הזמנה 
        {
            DO.Order dOrder = new DO.Order();

            try
            {
                dOrder = dal!.Order.GetById(OrderId);
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }

            if (dOrder.ShipDate == null)
                dOrder.ShipDate = DateTime.Now;
            dal.Order.Update(dOrder);

            return GetOrderById(OrderId);
        }

       /// <summary>
       /// update the delivery date 
       /// </summary>
       /// <param name="OrderId"></param>
       /// <returns>the updated date of the delivery </returns>
       /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.Order UpdateDelivertOrder(int OrderId)//עדכון אספקת הזמנה 
        {
            DO.Order dOrder = new DO.Order();

            try
            {
                dOrder = dal!.Order.GetById(OrderId);
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }

                if (dOrder.DeliveryDate == null)
                    dOrder.DeliveryDate = DateTime.Now;

                dal.Order.Update(dOrder);
            return GetOrderById(OrderId);
        }

        /// <summary>
        /// order trackin of each order by his ID
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.OrderTracking OrderTracking(int OrderId)
        {
            DO.Order dOrder = new DO.Order();
            try
            {
                dOrder = dal!.Order.GetById(OrderId);

            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }
            return new BO.OrderTracking()
            {
                ID = dOrder.ID,
                Status = GetStatus(dOrder),
                Tracking = new List<Tuple<DateTime?, string>>() { new Tuple<DateTime?, string>( dOrder.OrderDate, "order date "),///create the tuple 
                    new Tuple<DateTime?, string>( dOrder.ShipDate, "ship date "),
                    new Tuple<DateTime?, string>( dOrder.DeliveryDate, "delivery date ") }
            };
        }
/// <summary>
/// function which sort the orders by total price, need to check how can we use her in PL
/// </summary>
/// <param name="ascending"></param>
/// <returns></returns>
        public IEnumerable<IGrouping<double, BO.OrderForList>> GetGroupedOrderes(bool ascending = true)
        {
            if (ascending == true)
                return from order in GetOrders()
                       orderby order.TotalPrice
                         group order by order.TotalPrice into g
                         select g;
            else
                return from order in GetOrders()
                       orderby order.TotalPrice descending
                       group order by order.TotalPrice into g
                       select g;

        }
        
    }
    

}
