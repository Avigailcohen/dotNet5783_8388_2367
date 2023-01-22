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
        /// <returns>list of order </returns>
        public IEnumerable<BO.OrderForList> GetOrders()// return IEnumerable of orders for the manager בקשת רשימת הזמנות למנהל 
        {
            IEnumerable<DO.Order?> orders = dal!.Order.GetAll();//get the list of the orders from dal stage
            IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll();//get the list of the order items form dl stage

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

        public BO.Order GetOrderById(int OrderId)// order detalis for the managerבקשת פרטי הזמנה למנהל 
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
        /// update the ship date to dateTime.now 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>the updated date </returns>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        public BO.Order UpdateOrderShip(int OrderId)// update shipd date עדכון שילוח הזמנה 
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
        public BO.Order UpdateDelivertOrder(int OrderId)// update the delivery date עדכון אספקת הזמנה 
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
        /// <summary>
        /// function which allow the manager to update the amount of the prodcuts in thr cart 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="newAmount"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlIncorrectDateException"></exception>
        /// <exception cref="BO.BlIdDoNotExistException"></exception>
        /// <exception cref="BlIncorrectDateException"></exception>
        public BO.Order UpdateOrder(int orderId, int productId, int newAmount)
        {
            if (newAmount < 0)
                throw new BO.BlIncorrectDateException("Negative amount");
            DO.Order? order;
            try { order = dal?.Order.GetById(orderId); }
            catch (Exception ex)
            {
                throw new BO.BlIdDoNotExistException( "Order",ex);
            }
            if (order?.DeliveryDate !=null)
                throw new BO.BlIncorrectDateException("Order deliverd");
            DO.Product product;
            try { product = (DO.Product)dal!.Product.GetById(productId); }//for update in stock field
            catch (Exception ex) { throw new BO.BlIdDoNotExistException("Product", ex); }
            BO.Order? wantedOrder = GetOrderById(orderId);
            BO.OrderItem? oi = wantedOrder.OrderItems.FirstOrDefault(oi => oi?.ProductID == productId);
            if (product.InStock <= 0 || product.InStock < newAmount)
                throw new BO.BlIncorrectDateException("Negative detalis");
            if (newAmount > product.InStock - oi?.AmountOfItem)
                throw new BlIncorrectDateException("Negative detalis");
            if (newAmount != 0)
            {
                if (oi == null)//if he product is not in the order, add it
                {
                    DO.Product productHelp = dal.Product.GetById(productId);
                    IEnumerable<DO.OrderItem?> orderItems = dal?.OrderItem.GetAll()!;
                    oi = new BO.OrderItem()
                    {
                        AmountOfItem = newAmount,
                        Name = productHelp.Name,
                        Price = productHelp.Price,
                        ProductID = productId,
                        TotalPrice = newAmount * productHelp.Price,
                    };
                    DO.OrderItem add = new DO.OrderItem()//update in the data layer
                    {
                        ID = oi.ID,
                        Amount = oi.AmountOfItem,
                        OrderId= orderId,
                        Price = oi.Price,
                        ProductId = productId
                    };
                    wantedOrder?.OrderItems?.Append(oi);
                    dal?.OrderItem.Add(add);
                    product.InStock -= add.Amount;
                    dal?.Product.Update(product);//update the amount in stocp of product
                    return wantedOrder!;
                }
                //if the product has been in the order already
                product.InStock += oi.AmountOfItem;
                wantedOrder!.TotalPrice -= oi!.TotalPrice;//for calculate the new total price of the order
                oi.AmountOfItem = newAmount;
                oi.TotalPrice = newAmount * oi.Price;
                wantedOrder.TotalPrice += oi.TotalPrice;//for calculate the new total price of the order
                DO.OrderItem update = new DO.OrderItem()
                {
                    ID = oi.ID,
                    Amount = oi.AmountOfItem,
                    OrderId = orderId,
                    Price = oi.Price,
                    ProductId = productId
                };

                product.InStock -= oi.AmountOfItem;
                dal.Product.Update(product);
                dal?.OrderItem.Update(update);
                return wantedOrder;
            }
            else
            {
                product.InStock += oi!.AmountOfItem;
                dal.Product.Update(product);
                wantedOrder?.OrderItems?.Where(oi => oi?.ProductID == productId);
                dal?.OrderItem.Delete(oi.ID);
                if (wantedOrder?.OrderItems?.Count() == 0)
                {
                    dal?.Order.Delete(orderId);
                }
                return wantedOrder;
            }
        }
        

    }
    

}
