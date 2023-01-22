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
    /// Interaction logic for OrderTrack.xaml
    /// </summary>
    public partial class OrderTrack : Window
    {
        BIApi.IBl? bl = BIApi.Factory.Get();


        public BO.OrderTracking?  OrderTracking
        {
            get { return (BO.OrderTracking )GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingProperty =
            DependencyProperty.Register("OrderTracking", typeof(BO.OrderTracking ), typeof(Window), new PropertyMetadata(null));


        public OrderTrack(int id)
        {
            InitializeComponent();
            try
            {
                //get the information about the order status by the function 
                OrderTracking= bl?.Order.OrderTracking(id);
            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message + "  is not exist","Not exist", MessageBoxButton.OK,MessageBoxImage.Error);
                
            }
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            
        }

        //private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        //{
        //    int id=int.Parse(iDTextBox.Text);
        //    OrderTracking = bl?.Order.OrderTracking(id);
        //    bool mannager = false;
        //    new OneOrder(OrderTracking.ID, mannager).Show();
        //}

        private void Button_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            //movve the the order window- the boolean is flase now because the client wants to entire and the buttons should be hidden
            int id = int.Parse(iDTextBox.Text);
            OrderTracking = bl?.Order.OrderTracking(id);
            bool mannager = false;
            new OneOrder(OrderTracking.ID, mannager).Show();

        }
    }
}
