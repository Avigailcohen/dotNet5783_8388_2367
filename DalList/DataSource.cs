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
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 1000;
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
        string[,] Names = new string[,] {{ "midi Dress", " flower dress", " black dress" },
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
            product.Category = (Enums.Category)x;
            product.Name = Names[x, s_rand.Next(2)];
            //Name = Names(x, s_rand.Next(5)),
            product.InStock =InStocks[i];
            ProductList.Add(product);

        }




    }

    private static void createAndInitOrders()
    {
        string[] CustomerNames = { "Avigail", "Shira", "Daniel", "Noa", "Ari" };
        string[] CustomerAdrress = { "Akalir", "Geva", "Rainess", "Yerusalim" };

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            int x = s_rand.Next(5);


            order.ID = config.NextOrderNumber;
            order.CustomerName = CustomerNames[x];
            order.CustomerAddress = CustomerAdrress[s_rand.Next(4)];
            order.CustomerEmail = CustomerNames[x] + "@gmail.com";
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
            OrderList.Add(order);




        }
    }
    private static void createAndInitOrderItems()
    {

        for (int i = 0; i < 20;)
        {
            OrderItem itemAdd = new OrderItem();
            itemAdd.OrderId = 1000 + i;
            int x = s_rand.Next(1, 4);
            for (int j= 0; j < x; j++)
            {  
                itemAdd.ID = config.NextOrderItemNumber;
                itemAdd.ProductId=1000+s_rand.Next(0,10);
                itemAdd.Price = s_rand.Next(50, 500);
                itemAdd.Amount = s_rand.Next(1, 2);
                OrderItemList.Add(itemAdd);
            }
            i++;




        }
        
    }
    
}  
