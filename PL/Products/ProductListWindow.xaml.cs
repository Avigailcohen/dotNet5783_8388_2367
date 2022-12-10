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
using BIApi;
using BIImplementation;
using BO;



namespace PL.Products
{///לבדוק לגבי הפונקצה של העדכון\
 /// <summary>
 /// Interaction logic for ProductListWindow.xaml
 /// </summary>
    
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl();
        
        public ProductListWindow(IBl bl)
        {
            InitializeComponent();
            Category category = new BO.Category();
            Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
            listViewOfProducts.ItemsSource = bl.Product.GetListedProducts();
           
        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            
            Category category = (BO.Category)Category.SelectedItem;
            
            listViewOfProducts.ItemsSource = bl.Product.GetListedProducts(x => x?.Category == category);
            if (Category.SelectedIndex == 4)
                listViewOfProducts.ItemsSource = bl.Product.GetListedProducts();


        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            listViewOfProducts.ItemsSource = bl.Product.GetListedProducts();

        }

        //private void update(object sender, MouseButtonEventArgs e)
        //{
        //    if (listViewOfProducts.SelectedItem is ProductForList productForList)
        //        new Update(productForList).ShowDialog();
        //}

        private void update(object sender, MouseButtonEventArgs e)
        {
            int id = ((ProductForList)listViewOfProducts.SelectedItem).ID;
;            if (listViewOfProducts.SelectedItem is ProductForList productForList)
                new ProductWindow(id).ShowDialog();
            listViewOfProducts.ItemsSource = bl.Product.GetListedProducts();
        }
       
    }
}
