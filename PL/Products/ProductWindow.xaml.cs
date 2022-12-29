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
        public ProductWindow()
        {
            ///for add 
            /// when the admin press on add 
            InitializeComponent();
           
            selectionCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            AddOrUpBtn.Content = "Add";
            ID.Foreground = Brushes.Black;
            ID.BorderBrush = new SolidColorBrush(Colors.Gold);
            Name.BorderBrush = new SolidColorBrush(Colors.Gold);
            Price.BorderBrush = new SolidColorBrush(Colors.Gold);
            selectionCB.BorderBrush = new SolidColorBrush(Colors.Gold);
            Amount.BorderBrush = new SolidColorBrush(Colors.Gold);
            



        }
        /// <summary>
        /// ctor to UPDATE action 
        /// </summary>
        /// <param name="id"></param>
        public ProductWindow(int id)
        {
            InitializeComponent();
           

            selectionCB.ItemsSource = Enum.GetValues(typeof(BO.Category));///for the comboBox
            BO.Product product = bl.Product.GetProductsById(id);//getting the details from bl about the product 
            ID.Text = product.ProductID.ToString();// for the ID textBOX
            ID.IsReadOnly = true;// makes the textBox unable to change 
            ID.Foreground = Brushes.Black;
            Name.Text = product.Name;//for the ID textBOX
            selectionCB.Text = product.Category.ToString();//select category 
            Price.Text = product.Price.ToString();
            Amount.Text = bl.Product.GetProductsById(product.ProductID).InStock.ToString();// for the Amount 
            AddOrUpBtn.Content = "Update";// the button of Update 
            ID.BorderBrush = new SolidColorBrush(Colors.Gold);
            Name.BorderBrush = new SolidColorBrush(Colors.Gold);
            Price.BorderBrush = new SolidColorBrush(Colors.Gold);
            selectionCB.BorderBrush = new SolidColorBrush(Colors.Gold);
            Amount.BorderBrush = new SolidColorBrush(Colors.Gold);
        }

        
        /// <summary>
        /// there is a one button for update and add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addOrUp(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
           


            try
            {
                ///when the admin didnt fiil one of the textbox there is a masaage 
                if (ID.Text.Length == 0 || Price.Text.Length == 0 || Amount.Text.Length == 0 || Name.Text.Length == 0 || selectionCB.Text.Length == 0 || selectionCB.SelectedIndex == 4 || selectionCB.SelectedItem == null)
                {
                    ID.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    Name.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    Price.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    selectionCB.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    Amount.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    MessageBox.Show("Fill in the missing fields","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                    
                }
                

                ///adding a product to the list 
                if (AddOrUpBtn.Content == "Add")
                {
                    BO.Product product = new BO.Product()
                    {
                        ProductID = int.Parse(ID.Text),
                        Name = Name.Text,
                        InStock = int.Parse(Amount.Text),
                        Price = double.Parse(Price.Text),
                        Category = (BO.Category)selectionCB.SelectedItem
                    };
                    bl!.Product.AddProduct(product);
                   
                    messageBoxResult = MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                else
                {
                    ///when he press two clicks on the product for update the prodcut
                    bl!.Product.UpdateProductData(new Product()
                    {
                        ProductID = int.Parse(ID.Text),
                        Name = Name.Text,
                        InStock = int.Parse(Amount.Text),
                        Price = int.Parse(Price.Text),
                        Category = (BO.Category)selectionCB.SelectedItem

                    });
                    
                    messageBoxResult = MessageBox.Show("Product update succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (BO.BlIdAlreadyExistException x)
            {

                messageBoxResult = MessageBox.Show(x.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(x.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.BlInvalidInputException)
            {
                MessageBox.Show("press ID again ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            //switch (messageBoxResult)
            //{
            //    case MessageBoxResult.OK or MessageBoxResult.Cancel:
            //        //Close();
            //        break;

            //    default:
            //        break;
            //}

        }

        private void selectCategory(object sender, SelectionChangedEventArgs e)
        {

        }

      

        /// <summary>
        /// functions that makes that  the admin couldnt press letters and juct  numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ID_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;


            e.Handled = true;

            return;


        }

      

        private void Price_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void Amount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;


            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;



            e.Handled = true;



            return;

        }

       
    }
}
