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
    /// Interaction logic for OneOrder.xaml
    /// </summary>
    public partial class OneOrder : Window
    {
        static readonly BIApi.IBl? bl = BIApi.Factory.Get();


        public BO.Order? order
        {
            get { return (BO.Order?)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public bool Manager
        {
            get { return (bool)GetValue(ManagerProperty); }
            set { SetValue(ManagerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Manager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManagerProperty =
            DependencyProperty.Register("Manager", typeof(bool), typeof(OneOrder), new PropertyMetadata(null));





        //public Visibility visibility
        //{
        //    get { return (Visibility)GetValue(visibilityProperty); }
        //    set { SetValue(visibilityProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for visibility.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty visibilityProperty =
        //    DependencyProperty.Register("visibility", typeof(Visibility), typeof(OneOrder), new PropertyMetadata(null));


        public Visibility visibility { get; set; }




        public OneOrder(int id)
        {
            Manager = true;
            
            InitializeComponent();
            try
            {
                order = bl?.Order.GetOrderById(id);

            }
            catch(BO.BlIdDoNotExistException ex)
            {
                order = null;
                MessageBox.Show(ex.Message,"Operation Fail" , MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void Update_Delivery(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl?.Order.UpdateDelivertOrder(order!.ID);
                messageBoxResult= MessageBox.Show("Order delivered  succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                order = bl?.Order.GetOrderById(order!.ID);
                
            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                bl?.Order.UpdateOrderShip(order!.ID);
                messageBoxResult= MessageBox.Show("Order shipped  succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                order = bl?.Order.GetOrderById(order!.ID);
            }
            catch (BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        public OneOrder(int id,bool manngaer)
        {
            Manager = manngaer;
            InitializeComponent();
            if (manngaer == false)
            {

                order=bl?.Order.GetOrderById(id);
                
                //shipDateDatePicker.IsEnabled = false;
               // deliveryDateDatePicker.IsEnabled=false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int idForDelete = order.ID;
                order = bl.Order.UpdateOrder(Convert.ToInt32(iDTextBox.Text), Convert.ToInt32(prod.Text),Convert.ToInt32(amou.Text));
                MessageBox.Show("The order was successfully updated");
                orderItemListView.Items.Refresh();



            }
            catch(BO.BlIdDoNotExistException ex) { MessageBox.Show(ex.Message); }
            catch(BO.BlIncorrectDateException ex) { MessageBox.Show("couldnt do it"); }
        }
    }
}
