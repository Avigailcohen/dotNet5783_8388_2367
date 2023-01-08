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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Products;
using BL;
using System.Security.Cryptography;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BIApi.IBl? bl = BIApi.Factory.Get();


        public MainWindow()
        {
            InitializeComponent();

        }

        private void productList(object sender, RoutedEventArgs e)
        {
            new LogIn().Show();
        }




        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            MoveToT.Visibility = Visibility.Visible;
            EnterOrderID_.Visibility = Visibility.Visible;
            IDText.Visibility = Visibility.Visible;
        }

        private void IDText_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void MoveToT_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          int ID=int.Parse(IDText.Text);
           new OrderTrack(ID).Show();
            
            
           
        }

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.Cart cart = new BO.Cart
            {
                CustomerName = " ",
                CustomerAddress = " ",
                CustomerEmail = " ",
                OrderItems = new List<BO.OrderItem?>(),
                Price = 0
            };
            new CustomerWindow(cart).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void bind_MouseEnter(object sender, MouseEventArgs e)
        {
            bind.Width = 1000;
            bind.Height = 300;

        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            blue.Height = 1000;
            blue.Width = 300;
        }
    }
};
