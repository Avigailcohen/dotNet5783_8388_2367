using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for MyCart.xaml
    /// </summary>
    public partial class MyCart : Window
    {
        static readonly BIApi.IBl? bl = BIApi.Factory.Get();
        MessageBoxResult result;
        
        public BO.Cart MyCart1
        {
            get { return (BO.Cart)GetValue(MyCart1Property); }
            set { SetValue(MyCart1Property, value); }
        }


        // Using a DependencyProperty as the backing store for MyCart1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCart1Property =
            DependencyProperty.Register("MyCart1", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));
        public Visibility Visibility{ get; set; }

        public MyCart(BO.Cart cartY)
        {
            InitializeComponent();
            MyCart1 = cartY;

        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //result = MessageBox.Show("ARE YOU SURE YOU WANT TO COMPLETE YOUR ORDER?", "Confirm", MessageBoxButton.YesNo);
            //if (result == MessageBoxResult.Yes)
            //{
            //    try
            //    {
            //        bl?.Cart.OrderConfirmation(MyCart1);
            //        MessageBox.Show("Thank you, check your email and track after your order");
            //        this.Close();
            //    }
            //    catch (BO.BlInvalidInputException ex)

            //    {
            //        MessageBox.Show(ex.Message+ "One of the fields are incorrect");
            //        this.Close();


            //    }
            //    catch(BO.BlNullPropertyException ex)
            //    {
            //        MessageBox.Show("NO ITEMS IN CART");
            //        MessageBox.Show("try again");
            //        this.Close();
            //    }

                
            //}


            //else
            //{

            //}
            //MessageBox.Show("Thank you, check your email and track after your order");
            //this.Close();



        }

        //private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        BO.OrderItem orderItem = (BO.OrderItem)((TextBox)sender).DataContext;
        //        int productID = orderItem.ProductID;
        //        int amount = orderItem.AmountOfItem;
        //        MyCart1 = bl.Cart.UpdateAmount(MyCart1, productID, amount);
        //        orderItemListView.Items.Refresh();

        //    }
        //    catch (BO.BlIdDoNotExistException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    catch (BO.BlNullPropertyException ex)
        //    {
        //        MessageBox.Show(ex.message);
        //    }
        //}

        private void TextBox_TextChanged_1(object sender, RoutedEventArgs e)
        {
            try
            {
                
                BO.OrderItem orderItem = (BO.OrderItem)((TextBox)sender).DataContext;
                //int productID = orderItem.ProductID;
                //int amount = orderItem.AmountOfItem;
                MyCart1 = bl!.Cart.UpdateAmount(MyCart1, orderItem.ProductID, orderItem.AmountOfItem);
                orderItemListView.Items.Refresh();
                priceTextBox.Text=MyCart1.Price.ToString();
                
                
                

            }
            catch (BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message+"TRY AGAIN","ERROR",MessageBoxButton.OK,MessageBoxImage.Warning);
                
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.message+ " sorry 😞");
            }
        }

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            new CustomerWindow(MyCart1).Show();
        }

        private void Button_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            Visibility = Visibility.Visible;
            result = MessageBox.Show("Are you sure you want to copmplete your order?","CONFIRM",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    bl.Cart.OrderConfirmation(MyCart1);
                    MyCart1.OrderItems = new List<OrderItem>();
                    MyCart1.Price = 0;
                    //customerAddressTextBox.Text= "";
                    //customerEmailTextBox.Text = "";
                    //customerNameTextBox.Text = "";
                    orderItemListView.ItemsSource = null;
                    Payment.IsEnabled = false;
                    MessageBox.Show("Thank you, Track your order by the number which sent to your mail💕🙏");
                    //MyCart1.OrderItems=new List<OrderItem>();
                    //MyCart1.Price = 0;
                    //MyCart1.CustomerAddress = null;
                    //MyCart1.CustomerEmail = null;
                    //MyCart1.CustomerName = null;
                    //Payment.IsEnabled=false;
                }
                catch(BO.BlIncorrectDateException)
                {
                    MessageBox.Show("One or two fileds are InCorrect","ERROR",MessageBoxButton.OK);
                }
                catch(BO.BlNullPropertyException ex)
                {
                    MessageBox.Show(ex.message ,"ERROR", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
           
           

        }







        //private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        //{
        //    BO.OrderItem orderItem = (BO.OrderItem)((TextBox)sender).DataContext;
        //    //int productID = orderItem.ProductID;
        //    //int amount = orderItem.AmountOfItem;
        //    MyCart1 = bl.Cart.UpdateAmount(MyCart1, orderItem.ProductID, orderItem.AmountOfItem);
        //    orderItemListView.ItemsSource = MyCart1.OrderItems;
        //    priceTextBox.Text = MyCart1.Price.ToString();
        //}



        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    BO.OrderItem orderItem=(BO.OrderItem)((TextBox)sender).DataContext;
        //    int productID=orderItem.ProductID;
        //    int amount = orderItem.AmountOfItem;
        //    MyCart1=bl.Cart.UpdateAmount(MyCart1,productID,amount);
        //    OrderItem order = new OrderItem()
        //    {
        //        ProductID = productID,
        //        ID = productID,
        //        AmountOfItem = amount,
        //        Name = orderItem.Name,
        //        Price = orderItem.Price,
        //        TotalPrice = MyCart1.OrderItems.FirstOrDefault(p=>p.ID==p.ID).TotalPrice,

        //    };



        //}



    }
}
