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
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    }
    internal static List<Product?> ProductList { get; } = new List<Product?>();
    internal static List<Order?> OrderList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();

    }
    //static string [ ,]Names= new string[,]
    //{

    //}
    private static void createAndInitProducts()
    {
        //static string[,] Names{ "midi Dress"," flower dress",}
        {
            for (int i = 0; i < 10; i++)
            {
                int x = s_rand.Next(4);
                ProductList.Add(
                            new Product
                            {
                                ID = s_rand.Next(100000, 999999),
                                Price = s_rand.Next(50, 500),
                                Category = (Category)x,
                                //Name = Names(x, s_rand.Next(5)),
                                InStock = s_rand.Next(100),



                            });









            }




        } }

    private static void createAndInitOrders()
    {
        string[] CustomerNames = { "Avigail", "Shira", "Daniel", "Noa", "Ari" };
        string[] CustomerAdrress = { "Akalir", "Geva", "Rainess", "Yerusalim" };

        for (int i = 0; i < 20; i++)
        {
            int x = s_rand.Next(5);
            OrderList.Add(
                new Order
                {
                    ID = config.NextOrderNumber,
                    CustomerName = CustomerNames[x],
                    CustomerAddress = CustomerAdrress[s_rand.Next(4)],
                    CustomerEmail = CustomerNames[x] + "@gmail.com",
                    //OrderDate =


                });
        }
    }
    private static void createAndInitOrderItems()
    {

        for (int i = 0; i < 40; i++)
        {
            OrderList.Add(
                new OrderItem
                {
                    ID = config.NextOrderItemNumber,
                    OrderId =



                });


        }
    } } 
