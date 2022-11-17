using DO;
namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new Random();
    internal static class config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int nextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int nextOrderItemNumber { get => s_nextOrderItemNumber++; }

    }
    internal static List<Product?> productList { get; } = new List<Product?>();
    internal static List<Order?> orderList { get; } = new List<Order?>();
    internal static List<OrderItem?> orderItemList { get; } = new List<OrderItem?>();

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();

    }
    //static string [ ,]Names= new string[,]
    //{

    //}
    /// <summary>
    /// fill the list with product
    /// </summary>
    private static void createAndInitProducts()
    {
        string[,] names = new string[,] {{ "midi Dress", " flower dress", " black dress" },
                                          { "short skirt","midi skirt","maki skirt"},
                                          { "golf shirt","long shirt","short shirt"},
                                          { "belts","hats","bags"} ,
                                          {"boots","slippers","High Heels" } };
        int[] InStocks = { 13, 10, 5, 11, 9, 7, 44, 0, 55, 17, 23, 67, 12, 90, 66 };
        for (int i = 0; i < 10; i++)
        {
            int x = s_rand.Next(4);
            Product product = new Product();


            product.ID = s_rand.Next(100000 + i, 999999);
            product.Price = s_rand.Next(50, 500);
            product.Category = (Enums.Category)x;// convert the num for category
            product.Name = names[x, s_rand.Next(2)];
            //Name = Names(x, s_rand.Next(5)),
            product.InStock =InStocks[i];
            productList.Add(product);

        }




    }

    private static void createAndInitOrders()
    {
        /// fill the list of orders
        string[] customerNames = { "Avigail Cohen ", "Shira Levi", "Daniel alon", "Noa Roth", "Ari Yehoda" };
        string[] CustomerAdrress = { "Akalir 12 ", "Geva 37", "Rainess 30", "Yerusalim 13" };
        

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            int x = s_rand.Next(5);


            order.ID = config.nextOrderNumber;
            order.CustomerName = customerNames[x];
            order.CustomerAddress = CustomerAdrress[s_rand.Next(4)];
            order.CustomerEmail = customerNames[x] + "@gmail.com";
            order.OrderDate = DateTime.Now;
            if (i < 0.8 * 0.20)
            {
                order.ShipDate = order.OrderDate - TimeSpan.FromDays((double)s_rand.Next(4));
                if (i < 0.6 * 20)
                {
                    order.DeliveryDate = order.ShipDate - TimeSpan.FromDays((double)s_rand.Next(4));
                }
                else
                {
                    order.DeliveryDate = DateTime.MinValue;
                }
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }
            orderList.Add(order);




        }
    }
    private static void createAndInitOrderItems()
    {
        /// fill the list of orderItem

        for (int i = 0; i < 20;)
        {
            OrderItem itemAdd = new OrderItem();
            itemAdd.OrderId = 1000 + i;
            int x = s_rand.Next(1, 4);
            for (int j= 0; j < x; j++)
            {  
                itemAdd.ID = config.nextOrderItemNumber;
                itemAdd.ProductId=1000+s_rand.Next(0,10);
                itemAdd.Price = s_rand.Next(50, 500);
                itemAdd.Amount = s_rand.Next(1, 2);
                orderItemList.Add(itemAdd);
            }
            i++;




        }
        
    }
    
}  
