using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(PassText.Text=="123456" && UserName.Text=="Avigail@gmail.com")
            {
                new ChooseForM().Show();
            }
            else
            {
                PassText.BorderBrush=new SolidColorBrush(Colors.DarkRed);
                UserName.BorderBrush=new SolidColorBrush(Colors.DarkRed);
                lblWo.Visibility=Visibility.Visible;

            }
            if (PassText.Text.Length == 0)
                lblEmpty.Visibility = Visibility.Visible;
            if(UserName.Text.Length == 0)
                lblWe.Visibility=Visibility.Visible;
        }

        private void PassText_PreviewKeyDown(object sender, KeyEventArgs e)
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
    }
}
