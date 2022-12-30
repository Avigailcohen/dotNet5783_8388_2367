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
                OrderTracking= bl?.Order.OrderTracking(id);
            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message + "  is not exist","Not exist", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        
    }
}
