using System;
using System.Collections.Generic;
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
        public OrderList(BIApi.IBl? bl)
        {
            InitializeComponent();
            orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();
       
        }
        
        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? pfl = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if(pfl!=null)
            {
                OneOrder oneOrder = new OneOrder(pfl.ID);
                oneOrder.ShowDialog();
                orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();
                
            }

            //new OneOrder().Show();
          
        }
    }
}
