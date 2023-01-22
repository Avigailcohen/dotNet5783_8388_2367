using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Controls.Primitives;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for TrackMa.xaml
    /// </summary>
    public partial class TrackMa : Window
    {
        ProgressBar progbar = new ProgressBar();
       
        
        BackgroundWorker Worker;
        DateTime timeSim=DateTime.Now;
        BIApi.IBl? bl = BIApi.Factory.Get();
        BO.Order order;
        public  List<BO.OrderForList?> OrderForLists
        {
            get { return (List<BO.OrderForList?>)GetValue(OrderForListsProperty); }
            set { SetValue(OrderForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderForLists.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderForListsProperty =
            DependencyProperty.Register("OrderForLists", typeof(List<BO.OrderForList>), typeof(TrackMa), new PropertyMetadata(null));



        public TrackMa()
        {
            InitializeComponent();
            Worker=new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            OrderForLists= new List<BO.OrderForList>(bl!.Order.GetOrders());
            
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
                MessageBox.Show("Operation finished");  
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            
             OrderForLists = new List<BO.OrderForList>(bl!.Order.GetOrders());
            try
            {
                foreach (var item in OrderForLists)
                {
                    if (item.OrderStatus != BO.OrderStatus.Delivered)
                    {
                        order = bl.Order.GetOrderById(item.ID) ?? throw new Exception("order is null");
                        if (timeSim - order.OrderDate >= new TimeSpan(3, 0, 0, 0) && item.OrderStatus == BO.OrderStatus.Ordered)
                        {
                            order.OrderDate = timeSim.AddMinutes(1);
                            order = bl.Order.UpdateOrderShip(item.ID);

                        }
                        else if (timeSim - order.OrderDate >= new TimeSpan(6, 0, 0, 0) && item.OrderStatus == BO.OrderStatus.Shipped)
                        {
                            order.ShipDate = timeSim.AddDays(1);
                            order = bl.Order.UpdateDelivertOrder(item.ID);
                        }
                        OrderForLists = new List<BO.OrderForList>(bl!.Order.GetOrders())!;

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK);

            }
            //foreach(var item in OrderForLists)
            //{
            //    if(item.OrderStatus==BO.OrderStatus.Ordered)
            //    {
            //        bl.Order.UpdateOrderShip(item.ID);
            //    }
            //    if (item.OrderStatus == BO.OrderStatus.Shipped)
            //    {
            //        bl.Order.UpdateDelivertOrder(item.ID);
            //    }


        }
            //foreach (var order in orderForLists)
            //{
            //    if()
            //}
            //int precent = e.ProgressPercentage;


            //OrderForLists = new List<BO.OrderForList>(bl!.Order.GetOrders());

            //foreach (var order in orders)
            //{
            //    if (order)
            //}




            
        

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if(Worker.CancellationPending==true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(2000);
                    timeSim = timeSim.AddDays(1);
                    if(Worker.WorkerReportsProgress== true) { Worker.ReportProgress(11); }
                }
            }

            
        }

        

        private void OrderTrack(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? pfl = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if (pfl != null)
            {
                OrderTrack orderTrack = new OrderTrack(pfl.ID);
                orderTrack.ShowDialog();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.IsBusy != true)
            {
                Worker.RunWorkerAsync();
                //bwMarry.RunWorkerAsync(cbDays.SelectedValue);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Worker.WorkerSupportsCancellation == true)
                Worker.CancelAsync();
        }
    }
}
