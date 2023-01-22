using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
       static readonly BIApi.IBl? bl = BIApi.Factory.Get();




        public ObservableCollection<BO.Order> Orders
        {
            get { return (ObservableCollection<Order>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(int), typeof(OrderList), new PropertyMetadata(null));



        public OrderList(BIApi.IBl? bl)
        {
            InitializeComponent();
            orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();//get the list of the orders
            OrderStatus orderStatus= new BO.OrderStatus();
            Category1.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            Category1.SelectedIndex = 1;



        }
        
        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //double click on order to get information about her.
            BO.OrderForList? pfl = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if(pfl!=null)
            {
                OneOrder oneOrder = new OneOrder(pfl.ID);
                oneOrder.ShowDialog();
                orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();
                
            }

            //new OneOrder().Show();
          
        }


        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            OrderStatus orderStatus = (OrderStatus)((ComboBox)sender).SelectedItem;
            orderForListDataGrid.ItemsSource = bl.Order.GetOrders();



        }
    }
}
