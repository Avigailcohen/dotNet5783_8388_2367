using BIApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


        public Array Category
        {
            get { return (Array)GetValue(CategoryDep); }
            set { SetValue(CategoryDep, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryDep =
            DependencyProperty.Register("Category", typeof(Array), typeof(CustomerWindow));


        public ObservableCollection <ProductItem?>ProductItems
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(ProductItemsDep); }
            set { SetValue(ProductItemsDep, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItemsDep =
            DependencyProperty.Register("ProductItems", typeof(ObservableCollection<ProductItem?>), typeof(CustomerWindow));


        public CustomerWindow(BO.Cart cart)
        {
            InitializeComponent();
            Category = Enum.GetValues(typeof(Category));
            ProductItems = new ObservableCollection<ProductItem>(bl!.Product?.GetListedProductsForC())!;
            Cart1 = cart;
        }
    
        

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            new MyCart(Cart1).ShowDialog();
           



        }

        private void productItemListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = (BO.ProductItem?)((ListView)sender).SelectedItem;
            if (productItem != null)
            {
                OneProductItem oneProductItem = new OneProductItem(Cart1, productItem.ID);
                oneProductItem.ShowDialog();
            }
        }

        

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Category)((ComboBox)sender).SelectedItem;
            ProductItems = null;
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == selected));
            //ProductItems = new ObservableCollection<ProductItem?>(bl.Product.GetListedProductsForC());
        }
       
    }

}
