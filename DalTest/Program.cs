using Dal;
using DalApi;
using DO;

/// The main program which check the function of the crud and the dataSource
namespace DalTest
{

    public class program
    {
        //private static DalOrder dalOrder = new DalOrder();
        //private static DalProduct dalProduct = new DalProduct();
        //private static DalOrderItem dalOrderItem = new DalOrderItem();
       static  IDal? dal = Factory.Get();


        /// <summary>
        /// productcheck function which check the function of the ProductCrud.
        /// </summary>
        static void ProductCheck()
        {
            int id;
            string name;
            float Price;
            int Instock;
            char choice1;

            Console.WriteLine("Enter one of the following options");
            Console.WriteLine($@"
press a for Add,
Press b for show the product,
press c for update the product,
press d for Delete the product,
press e for get the list of the product,
press f for return to the menue");

            char.TryParse(Console.ReadLine(), out choice1);
            while (choice1 != 'f')
            {
                switch (choice1)
                {
                    case 'a':
                        ///add product by get the name, price, amount in stock and category
                        Product product = new Product();
                        Console.WriteLine("Enter your ID");
                        int.TryParse(Console.ReadLine(), out id);
                        product.ID = id;
                        Console.WriteLine("Enter the product Name");
                        name = Console.ReadLine();
                        product.Name = name;
                        Console.WriteLine("Enter the product price");
                        float.TryParse(Console.ReadLine(), out Price);
                        product.Price = Price;
                        Console.WriteLine("Enter the product Stock");
                        int.TryParse(Console.ReadLine(), out Instock);
                        product.InStock = Instock;
                        Console.WriteLine("Enter the product category");
                        product.Category = (Category)Console.Read() - '1';
                        ///put the function inside try because thehe is risk that the product exist
                        try
                        {
                            dal?.Product.Add(product);
                            Console.WriteLine(product.ID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }


                        //add category

                        break;
                    case 'b':
                        /// get the product by his ID
                        Console.WriteLine("enter the product ID");
                        int.TryParse(Console.ReadLine(), out id);
                        Product product1 = new Product();
                        try
                        {
                            product1 = dal.Product.GetById(id);
                            Console.WriteLine(product1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        break;
                    case 'c':
                        /// update product 
                        Product product2 = new Product();
                        Console.WriteLine("Enter product ID: ");
                        int.TryParse(Console.ReadLine(), out id);
                        product2.ID = id;
                        Console.WriteLine("Enter product name: ");
                        name = Console.ReadLine();
                        product2.Name = name;
                        Console.WriteLine("Enter product price: ");
                        float.TryParse(Console.ReadLine(), out Price);
                        product2.Price = Price;
                        Console.WriteLine("Enter product amount in stock: ");
                        int.TryParse(Console.ReadLine(), out Instock);
                        product2.InStock = Instock;
                        /// there is risk that the product not exist 
                        try
                        {
                            dal?.Product.Update(product2);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        //add category
                        break;
                    case 'd':
                        /// delete product by his ID
                        Console.WriteLine("Enter product ID: ");
                        int.TryParse(Console.ReadLine(), out id);
                        /// there is risk that the product not exist
                        try
                        {
                            dal!.Product.Delete(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }


                        break;
                    case 'e':
                        /// print the product infomation
                        IEnumerable<Product?> list = dal.Product.GetAll();
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'f':
                        return;
                    default:
                        Console.WriteLine("the end of the menue");
                        break;
                }
                char.TryParse(Console.ReadLine(), out choice1);



            }

        }
        /// <summary>
        /// function which check the Order function (crud)
        /// </summary>
        static void OrderCheck()
        {
            Order order = new Order();
            char choice2;
            int id;
            string? name;
            string? email;
            string? address;
            DateTime orderDate = DateTime.Now;
            //DateTime deliveryTime = null;
            //DateTime shipDate=null;
            Console.WriteLine("Enter one of the following options");
            Console.WriteLine($@"
press a for Add,
Press b for show the Order,
press c for update the Order,
press d for Delete the Order,
press e for get the list of the Order
press f to return the menue");
            char.TryParse(Console.ReadLine(), out choice2);
            while (choice2 != 'g')
            {
                switch (choice2)
                {
                    case 'a':
                        ///add order to the list
                        Console.WriteLine("enter the Order ID");
                        int.TryParse(Console.ReadLine(), out id);
                        order.ID = id;
                        Console.WriteLine("Enter the customer Name");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter your Email");
                        email = Console.ReadLine();
                        order.CustomerEmail = email;
                        Console.WriteLine("Enter your Address");
                        address = Console.ReadLine();
                        order.CustomerAddress = address;
                        order.OrderDate = DateTime.Now;
                        dal!.Order.Add(order);
                        Console.WriteLine(order.ID);
                        //check what she return
                        break;
                    case 'b':
                        /// get the order by his ID
                        Order order1 = new Order();
                        Console.WriteLine("enter the Order ID");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            order1 = dal.Order.GetById(id);
                            Console.WriteLine(order1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case 'c':
                        /// update the order
                        Order order2 = new Order();
                        Console.WriteLine("enter the Order ID");
                        int.TryParse(Console.ReadLine(), out id);
                        order2.ID = id;
                        Console.WriteLine("Enter the customer Name");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter your Email");
                        email = Console.ReadLine();
                        order2.CustomerEmail = email;
                        Console.WriteLine("Enter your Address");
                        address = Console.ReadLine();
                        order2.CustomerAddress = address;
                        order2.OrderDate = DateTime.Now;
                        try
                        {
                            dal!.Order.Update(order2);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        break;
                    case 'd':
                        /// delete the Order
                        Console.WriteLine("enter the Order ID");
                        int.TryParse(Console.ReadLine(), out id);
                        /// there is risk the the order not exist
                        try
                        {
                            dal?.Order.Delete(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case 'e':
                        /// print all the list of order
                        IEnumerable<Order?> list = dal!.Order.GetAll();
                        foreach (Order? item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'f':
                        return;
                    default:
                        Console.WriteLine("the end of the menue");
                        break;
                }
                char.TryParse(Console.ReadLine(), out choice2);
            }



        }
        /// <summary>
        /// function which check the OrderItem function (crud)
        /// </summary>
        static void OrderItemCheck()
        {
            int id;
            int orderId;
            int productId;
            float price;
            int amount;
            char choice3;
            Console.WriteLine("Enter one of the following options");
            Console.WriteLine($@"
press a for Add,
Press b for show the OrderItem,
press c for update the OrderItem,
press d for Delete the OrderItem,
press e for get the list of the Order,
press f for show the list of all the orderItem that belong to the order,
press g for find the item in the order
press h for return to the menue");
            char.TryParse(Console.ReadLine(), out choice3);
            while (choice3 != 'i')
            {
                switch (choice3)
                {
                    case 'a':
                        ///add orderItem
                        OrderItem orderItem = new OrderItem();
                        Console.WriteLine("Enter the orderID");
                        int.TryParse(Console.ReadLine(), out orderId);
                        orderItem.OrderId = orderId;
                        Console.WriteLine("enter the productID");
                        int.TryParse(Console.ReadLine(), out productId);
                        orderItem.ProductId = productId;
                        Console.WriteLine("enter the price of OrderItem");
                        float.TryParse(Console.ReadLine(), out price);
                        orderItem.Price = price;
                        Console.WriteLine("enter the amount of OrderItem");
                        int.TryParse(Console.ReadLine(), out amount);
                        orderItem.Amount = amount;
                        dal?.OrderItem.Add(orderItem);
                        Console.WriteLine(orderItem.ID);
                        break;
                    case 'b':
                        OrderItem orderItem1 = new OrderItem();
                        Console.WriteLine("enter the OrderItem ID");
                        int.TryParse(Console.ReadLine(), out id);
                        /// get the orderItem by his ID
                        try
                        {
                            orderItem1 = dal!.OrderItem.GetById(id);
                            Console.WriteLine(orderItem1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        break;
                    case 'c':
                        ///update the orderITEM
                        OrderItem orderItem2 = new OrderItem();
                        Console.WriteLine("enter the OrderItem ID");
                        int.TryParse(Console.ReadLine(), out id);
                        orderItem2.ID = id;
                        Console.WriteLine("Enter the orderID");
                        int.TryParse(Console.ReadLine(), out orderId);
                        orderItem2.OrderId = orderId;
                        Console.WriteLine("enter the productID");
                        int.TryParse(Console.ReadLine(), out productId);
                        orderItem2.ProductId = productId;
                        Console.WriteLine("enter the price of OrderItem");
                        float.TryParse(Console.ReadLine(), out price);
                        orderItem2.Price = price;
                        //check

                        Console.WriteLine("enter the amount of OrderItem");
                        int.TryParse(Console.ReadLine(), out amount);
                        orderItem2.Amount = amount;
                        try
                        {
                            dal?.OrderItem.Update(orderItem2);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case 'd':
                        /// delete the OrderItem
                        Console.WriteLine("enter the OrderItem ID");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            dal!.OrderItem.Delete(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        break;
                    case 'e':
                        /// print all the OrderItem
                        IEnumerable<OrderItem?> list = dal!.OrderItem.GetAll();
                        foreach (OrderItem? item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'f'://לשאול את אפרת לגבי זה
                        /// print the orderItem that belong to this order
                        Console.WriteLine("enter the OrderItem ID");
                        int.TryParse(Console.ReadLine(), out id);
                        List<OrderItem?> orderItem4 = new List<OrderItem?>();
                        orderItem4 = dal!.OrderItem.GetAllOrderItems(id);
                        foreach (OrderItem? item in orderItem4)
                            Console.WriteLine(item);

                        break;
                    case 'g':
                        /// print the OrderItem by productID and OrderItemID
                        Console.WriteLine("enter the orderID,ProductID");
                        int.TryParse(Console.ReadLine(), out orderId);
                        int.TryParse(Console.ReadLine(), out productId);
                        OrderItem orderItem5 = new OrderItem();
                        orderItem5.OrderId = orderId;
                        orderItem5.ProductId = productId;
                        try
                        {
                            Console.WriteLine(dal?.OrderItem.GetOrderItem(orderId, productId));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("the object is not found" + ex);
                        }
                        break;
                    case 'h':
                        return;
                }
                char.TryParse(Console.ReadLine(), out choice3);
            }




        }



        static void Main(string[] args)
        {
            /// main program which check the function
            program program = new program();
            char choice;
            Console.WriteLine("enter one of the following options");
            Console.WriteLine($@"
press 1 for Order,
press 2 for product,
press 3 for OrderItem,
press 4 for exit");
            char.TryParse(Console.ReadLine(), out choice);

            while (choice != '5')
            {

                switch (choice)
                {
                    case '1':
                        program.OrderCheck();

                        break;
                    case '2':
                        program.ProductCheck();

                        break;
                    case '3':
                        program.OrderItemCheck();
                        break;

                    case '4':
                        return;


                }
                Console.WriteLine($@"
press 1 for Order,
press 2 for product,
press 3 for OrderItem,
press 4 for exit");
                char.TryParse(Console.ReadLine(), out choice);

            }

            //                                case '5':
            //                                    IEnumerable< Order? >list = new List<Order?>();
            //                                    foreach (var item in list)
            //                                    {
            //                                        Console.WriteLine(item?.ToString());
            //                                    }

            //                                    break;
            //                                default: Console.WriteLine("finish");
            //                                    break;

            //                            }




            //                            break;
            //                    }


            //                }
            //                catch ( FormatException str)
            //                {
            //                    Console.WriteLine(str);
            //                }


            //            }
            //        }
        }



    }
}
