using BIApi;
using BIImplementation;
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
using BO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    /// לבדוק את החריגה בupdate מה אני אמורה לזרוק ואיפה .
    /// ואיך מטלפים בחריגה של add שלרת תזרוק לי 2 שגיאות 
    /// 
    public partial class ProductWindow : Window
    {
        BIApi.IBl? bl = BIApi.Factory.Get();
        /// <summary>
        /// ctor to ADD action 
        /// </summary>
        ///

        MessageBoxResult result1;
        public BO.Product Product
        {
            get { return (BO.Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));
        public bool addOrUpdate
        {
            get { return (bool)GetValue(addOrUpdateProperty); }
            set { SetValue(addOrUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for addOrUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty addOrUpdateProperty =
            DependencyProperty.Register("addOrUpdate", typeof(bool), typeof(Window), new PropertyMetadata(null));
        public string? ctc { get; set; }//for the content on the button
        public Array Categories { get { return Enum.GetValues(typeof(Category)); } }



        public ProductWindow(bool flag)
        {
            ///for add 
            /// when the admin press on add 
            InitializeComponent();
            addOrUpdate = flag;//get the flag from the list of prodcut window true-update false-add
            if (!addOrUpdate)
            ctc = "Add";
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));
            delBtn.Visibility = Visibility.Collapsed;

            //selectionCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //AddOrUpBtn.Content = "Add";
            //ID.Foreground = Brushes.Black;
            //ID.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Name.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Price.BorderBrush = new SolidColorBrush(Colors.Gold);
            //selectionCB.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Amount.BorderBrush = new SolidColorBrush(Colors.Gold);




        }
        /// <summary>
        /// ctor to UPDATE action 
        /// </summary>
        /// <param name="id"></param>
        public ProductWindow(int id,bool flag)
        {
            InitializeComponent();
            addOrUpdate = flag;
            if (addOrUpdate)
            ctc = "Update";
            Product = bl!.Product.GetProductsById(id);
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));


            //selectionCB.ItemsSource = Enum.GetValues(typeof(BO.Category));///for the comboBox
            //BO.Product product = bl.Product.GetProductsById(id);//getting the details from bl about the product 
            //ID.Text = product.ProductID.ToString();// for the ID textBOX
            //ID.IsReadOnly = true;// makes the textBox unable to change 
            //ID.Foreground = Brushes.Black;
            //Name.Text = product.Name;//for the ID textBOX
            //selectionCB.Text = product.Category.ToString();//select category 
            //Price.Text = product.Price.ToString();
            //Amount.Text = bl.Product.GetProductsById(product.ProductID).InStock.ToString();// for the Amount 
            //AddOrUpBtn.Content = "Update";// the button of Update 
            //ID.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Name.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Price.BorderBrush = new SolidColorBrush(Colors.Gold);
            //selectionCB.BorderBrush = new SolidColorBrush(Colors.Gold);
            //Amount.BorderBrush = new SolidColorBrush(Colors.Gold);
        }

        
        /// <summary>
        /// there is a one button for update and add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void addOrUp(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult messageBoxResult;
           


        //    try
        //    {
        //        ///when the admin didnt fiil one of the textbox there is a masaage 
        //        if (productIDTextBox.Text.Length == 0 || priceTextBox.Text.Length == 0 || inStockTextBox.Text.Length == 0 || nameTextBox.Text.Length == 0 || categoryComboBox.Text.Length == 0 || categoryComboBox.SelectedIndex == 4)
        //        {
        //            ID.BorderBrush = new SolidColorBrush(Colors.DarkRed);
        //            Name.BorderBrush = new SolidColorBrush(Colors.DarkRed);
        //            Price.BorderBrush = new SolidColorBrush(Colors.DarkRed);
        //            selectionCB.BorderBrush = new SolidColorBrush(Colors.DarkRed);
        //            Amount.BorderBrush = new SolidColorBrush(Colors.DarkRed);
        //            MessageBox.Show("Fill in the missing fields","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
        //            return;
                    
        //        }
                

        //        ///adding a product to the list 
        //        if (AddOrUpBtn.Content == "Add")
        //        {
                    
        //            bl!.Product.AddProduct(Product);
                   
        //            //messageBoxResult = MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
        //            this.Close();

        //        }
        //        else
        //        {
        //            ///when he press two clicks on the product for update the prodcut
        //            bl!.Product.UpdateProductData(Product);
                    
                    
        //            messageBoxResult = MessageBox.Show("Product update succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
        //            this.Close();
        //        }
        //    }
        //    catch (BO.BlIdAlreadyExistException x)
        //    {

        //        messageBoxResult = MessageBox.Show(x.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //        MessageBox.Show(x.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    catch(BO.BlInvalidInputException)
        //    {
        //        MessageBox.Show("press ID again ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
            

        //    //switch (messageBoxResult)
        //    //{
        //    //    case MessageBoxResult.OK or MessageBoxResult.Cancel:
        //    //        //Close();
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}

        //}

        //private void selectCategory(object sender, SelectionChangedEventArgs e)
        //{

        //}

      

        /// <summary>
        /// functions that makes that  the admin couldnt press letters and juct  numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

      

       
        private void AddOrUpBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (!addOrUpdate/*(string)btnAddOrUpdate.Content == "Add"*/)
            {
                
                
                if (productIDTextBox.Text.Length == 0 || priceTextBox.Text.Length == 0 || inStockTextBox.Text.Length == 0 || nameTextBox.Text.Length == 0 || categoryComboBox.Text.Length == 0 || categoryComboBox.SelectedIndex == 4)
                {
                    productIDTextBox.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    nameTextBox.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    priceTextBox.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    categoryComboBox.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    inStockTextBox.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    MessageBox.Show("Fill in the missing fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
                BO.Product product = new BO.Product();
                product.ProductID = int.Parse(productIDTextBox.Text);
                product.Name = nameTextBox.Text;
                product.Price = int.Parse(priceTextBox.Text);
                product.Category = (Category)categoryComboBox.SelectedItem;
                product.InStock = int.Parse(inStockTextBox.Text);
                ////delBtn.IsEnabled=false;
                try
                {
                   bl?.Product.AddProduct(product);
                    Close();
                }
                // in case the adding faild
                catch (BO.BlIdAlreadyExistException ex)
                {

                   result1 = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlInvalidInputException)
                {
                    MessageBox.Show("press ID again ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                
                try
                {
                    bl?.Product.UpdateProductData(Product);
                    Close();
                }
                // in case the updatung faild
                catch (BO.BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
           
            
                
        }
        

        


        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bl?.Product.DeleteProduct(Product.ProductID);

            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Are you sure you want to delete this product?", "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(ex.Message + " " +"deleted succesfuly","DELETE",MessageBoxButton.OK,MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    Close();
                }
               
            }
        }

        private void inStockTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;

        }

        private void priceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;
        }

        private void productIDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;
        }
    }
}
