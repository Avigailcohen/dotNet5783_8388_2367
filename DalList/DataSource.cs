using DO;
namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new Random(DateTime.Now.Millisecond);
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
        //string[,] picturs=new string[,] { { } }
        string[,] names = new string[,] {{ "midi Dress", " flower dress", " black dress","long dress","Knit dress" },
                                          { "short skirt","midi skirt","maxi skirt","Knit skirt","Sheath skirt"},
                                          { "golf shirt","long shirt","short shirt","Knit shirt","Cashmere shirt"},
                                          { "belts","hats","bags", "glasses","bracelet" } ,
                                          {"White Sport shirt","Black Sport skirt","Black Sport shirt","White Sport skirt","Pink Sport shirt" } };
        int[] InStocks = { 13, 10, 5, 11, 9, 7, 44, 0, 55, 17, 23, 67, 12, 90, 66 };
        for (int i = 0; i < 15; i++)
        {
            int x = s_rand.Next(5);
            Product product = new Product();


            product.ID = 10000 + i;
            product.Price = s_rand.Next(50, 500);
            product.Category = (Category)x;// convert the num for category
            product.Name = names[x, s_rand.Next(5)];
            //Name = Names(x, s_rand.Next(5)),
            product.InStock = InStocks[i];
            productList.Add(product);

        }




    }

    private static void createAndInitOrders()
    {
        /// fill the list of orders
        string[] customerNames = { "Avigail Cohen ", "Shira Levi", "Daniel alon", "Noa Roth", "Ari Yehoda","Inbal cohen","Tamar katz","Avishag raviv","Tehila Gross" };
        string[] CustomerAdrress = { "Akalir 12 ", "Geva 37", "Rainess 30", "Yerusalim 13","Witzman 17","Avarham Shpapira 10","Levontin 39","Ywhoda Hanasi 10","Ben Gurion 1" };

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            int x = s_rand.Next(8);

            
            order.ID = config.nextOrderNumber;
            order.CustomerName = customerNames[x];
            order.CustomerAddress = CustomerAdrress[x];
            order.CustomerEmail = customerNames[x] + "@gmail.com";
            //int month = -s_rand.Next(1, 12);
           
            order.OrderDate =DateTime.Now+ new TimeSpan(s_rand.Next(1,4),s_rand.Next(24),s_rand.Next(s_rand.Next(60)));
            if (i < 10)
            {
                order.ShipDate = null;
                order.DeliveryDate = null;
            }
            else
            {
                order.ShipDate = order.OrderDate?.AddDays(s_rand.Next(2,4));
                //order.ShipDate=order.OrderDate+new TimeSpan(s_rand.Next(1,4),s_rand.Next(24),s_rand.Next(60));
                if (i < 15)
                {
                    order.DeliveryDate=null;
                }
                else
                {
                    
                    order.DeliveryDate = order.ShipDate?.AddDays(s_rand.Next(6,8));
                }
            }
            orderList.Add(order);
        }
    }
    private static void createAndInitOrderItems()
    {
        /// fill the list of orderItem

        for (int i = 0; i < 40;)
        {
            OrderItem itemAdd = new OrderItem();
            Product product = new Product();

            if (i < 20)
                itemAdd.OrderId = orderList[i]!.Value.ID;
            else
                itemAdd.OrderId = orderList[s_rand.Next(0, orderList.Count())]!.Value.ID;
            int x = s_rand.Next(1, 4);
            for (int j = 0; j < x; j++)
            {
                itemAdd.ID = config.nextOrderItemNumber;
                product = productList[s_rand.Next(0, productList.Count())]!.Value;
                itemAdd.ProductId = product.ID;
                itemAdd.Amount = s_rand.Next(1, 4);
                itemAdd.Price = product.Price * itemAdd.Amount;
                orderItemList.Add(itemAdd);
            }
            i++;
        }

    }

}
