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


        public CustomerWindow(BO.Cart cart)//get cart in ctor for the next window..
        {
            InitializeComponent();
            Category = Enum.GetValues(typeof(Category));
            ProductItems = new ObservableCollection<ProductItem>(bl!.Product?.GetListedProductsForC()!)!;//get the list of the prodcut item (catalog)
            Cart1 = cart;
            ComboC.Text = "All";//מיותר לזכור להוריד
        }
    
        

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            new MyCart(Cart1).ShowDialog();//move to the next window with the same cart for the order confirmation
         }
        /// <summary>
        /// double click for get the detalis of the prodcut 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productItemListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = (BO.ProductItem?)((ListView)sender).SelectedItem;
            if (productItem != null)
            {
                OneProductItem oneProductItem = new OneProductItem(Cart1, productItem.ID);
                oneProductItem.ShowDialog();
            }
        }

        /// <summary>
        /// מיותר להוריד את זה 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Category)((ComboBox)sender).SelectedItem;
            ProductItems = null;
            if (selected == BO.Category.All)
            {
                ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC());
                return;
            }

            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == selected));
            //ProductItems = new ObservableCollection<ProductItem?>(bl.Product.GetListedProductsForC());
        }

       /// <summary>
       /// for get only the Accessories items
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            ProductItems=new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == BO.Category.Accessories));
        }


        /// <summary>
        /// for get only the sports items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Label_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == BO.Category.Sport));
        }

        /// <summary>
        /// for get only the skirts items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Label_MouseEnter_2(object sender, MouseEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == BO.Category.Skirts));
        }
        /// <summary>
        /// for get only the shirts items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseEnter_3(object sender, MouseEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == BO.Category.Shirts));
        }
        /// <summary>
        /// for get only the dresses items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseEnter_4(object sender, MouseEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC().Where(x => x?.Category == BO.Category.Dresses));
        }
        /// <summary>
        /// for get the whole list o the prodcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Label_MouseEnter_5(object sender, MouseEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.GetListedProductsForC());
        }
        /// <summary>
        /// for get grouped list of the most expensice prodcut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxbSortByCategory_Checked(object sender, RoutedEventArgs e)
        {
            ProductItems = new ObservableCollection<ProductItem?>(bl!.Product.MostExpensive(Cart1));
        }

        
    }

}
