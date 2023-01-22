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
        /// <summary>
        /// bool dp because there is one window which the client and the manager could get.
        /// </summary>
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



        /// <summary>
        /// getting the id of the order in ctor and the dp is true becayse the entire is from the list of the orders
        /// </summary>
        /// <param name="id"></param>
        public OneOrder(int id)
        {
            //manger entrie
            Manager = true;
            
            InitializeComponent();
            try
            {
                //get the detalis of the order by thr function 
                order = bl?.Order.GetOrderById(id);

            }
            catch(BO.BlIdDoNotExistException ex)
            {
                order = null;
                MessageBox.Show(ex.Message,"Operation Fail" , MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
        //because this window can be opened by the manager and by the client i creatd a boolean dp which in the xml i made the difference between them.
        //when the manger gets into this window he has 3 changes he can do- update the amoun of prodcut and update the delivery/ship date.
        //so when the client gets into his order i dont allow him to do it so i used binding between the boolean dp in order to do that.
        private void Update_Delivery(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                //update delivery date
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
                //update shipment date
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
            //when client wants to see his order so the buttons of the update are hidden- the dp is false
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
            //check if the fields  of the text box are empty
            //if (prod.Text.Length == 0 || amou.Text.Length == 0)
            //    MessageBox.Show("Nothing to update❌", "EMPTY", MessageBoxButton.OK, MessageBoxImage.Warning);
            ////return;
            try
            {
                //for the manger-update the amount
                int idForDelete = order.ID;
                //send the amount we wrote in the text box and send it as parameter by convert it.
                order = bl.Order.UpdateOrder(Convert.ToInt32(iDTextBox.Text), Convert.ToInt32(prod.Text),Convert.ToInt32(amou.Text));
                MessageBox.Show("The order was successfully updated");
                //refresh the list of the items after the changes.
                orderItemListView.Items.Refresh();



            }
            catch(BO.BlIdDoNotExistException ex) { MessageBox.Show(ex.Message); }
            catch(BO.BlIncorrectDateException ex) { MessageBox.Show("The operation failed","ERROR",MessageBoxButton.OK,MessageBoxImage.Exclamation); }
        }
    }
}
