using BIApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BIApi.IBl? bl = BIApi.Factory.Get();
        BO.Cart Cart1;

        public CustomerWindow(BO.Cart cart)
        {

            InitializeComponent();
            productItemListView.ItemsSource = bl?.Product?.GetListedProductsForC();
            Cart1 = cart;

        }
        

        private void productItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = productItemListView.SelectedItem as BO.ProductItem;
            if(productItem != null)
            {
                OneProductItem oneProductItem = new OneProductItem(Cart1, productItem.ID);
                oneProductItem.ShowDialog();
                
            }
        }

        

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            new MyCart(Cart1).ShowDialog();
           



        }
    }

}
