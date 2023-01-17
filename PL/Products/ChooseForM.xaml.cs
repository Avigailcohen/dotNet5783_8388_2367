﻿
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
    /// Interaction logic for ChooseForM.xaml
    /// </summary>
    public partial class ChooseForM : Window
    {
          static readonly BIApi.IBl? bl = BIApi.Factory.Get();
        public ChooseForM()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
        }

        

        private void OrderList_(object sender, RoutedEventArgs e)
        {
            new OrderList(bl).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new TrackMa().Show();
        }
    }
}
