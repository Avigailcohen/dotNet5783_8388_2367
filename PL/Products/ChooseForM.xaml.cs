
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
    /// Interaction logic for ChooseForM.xaml
    /// </summary>
    public partial class ChooseForM : Window
    {
          static readonly BIApi.IBl? bl = BIApi.Factory.Get();
        public ChooseForM()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///move to the list of the prodcut window 
            new ProductListWindow(bl).Show();
        }

        /// <summary>
        /// move to the list of the orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OrderList_(object sender, RoutedEventArgs e)
        {
            new OrderList(bl).Show();
        }
        /// <summary>
        /// move to the simulator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new TrackMa().Show();
        }
    }
}
