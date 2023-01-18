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
    /// Interaction logic for LogInS.xaml
    /// </summary>
    /// 
    public partial class LogInS : Window
    {

        static readonly BIApi.IBl? bl = BIApi.Factory.Get();
        public BO.User  User
        {
            get { return (BO.User )GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(BO.User ), typeof(LogInS), new PropertyMetadata(null));


        public LogInS()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(userNmae.Text.Length==0||PassW.Password.Length==0)
            {
                userNmae.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                PassW.BorderBrush=new SolidColorBrush(Colors.DarkRed);
            }
               

            try
            {
                User = bl.User.LogIn(userNmae.Text, PassW.Password);
                if (User.status == BO.Status.Mannager)
                    this.Close();
                    new ChooseForM().Show();
                

            }
            catch(BO.BlIncorrectDateException ex)
            {
                MessageBox.Show("Try Again","ERROR",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                new MainWindow().Show();
                
            }
            catch(BO.BlIdDoNotExistException ex)
            {
                MessageBox.Show("ERROR");
            }
        }
    }
}
