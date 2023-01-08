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
    /// Interaction logic for OneProductItem.xaml
    /// </summary>
    public partial class OneProductItem : Window
    {
        static readonly BIApi.IBl? bl = BIApi.Factory.Get();
        BO.Cart cart;


        //public BO.Product Product
        //{
        //    get { return (BO.Product)GetValue(ProductProperty); }
        //    set { SetValue(ProductProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ProductProperty =
        //    DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));



        public BO.ProductItem ProductItem
        {
            get { return (BO.ProductItem)GetValue(ProductItemProperty); }
            set { SetValue(ProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItemProperty =
            DependencyProperty.Register("ProductItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));




        public OneProductItem()
        {
            InitializeComponent();

            
        }
        public OneProductItem( BO.Cart cart,int ID)
        {
            InitializeComponent();
            try
            {
                ProductItem = bl?.Product?.RequestProductDetailsForC(ID, cart);
            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show(ex.Message+"try again");
            }
            this.cart = cart;

        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int ID=int.Parse(iDTextBlock.Text);
            try
            {
                cart = bl.Cart.AddProduct(cart, ID);
            }
            catch(BO.BlNullPropertyException)
            {
                MessageBox.Show("NOT IN STOCK","OUT OF STOCK",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new CustomerWindow(cart).ShowDialog();
        }

       
    }
}
