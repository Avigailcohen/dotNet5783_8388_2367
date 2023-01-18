using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Products;
using BO;
using System.Windows.Media;
using System.Windows.Controls;

namespace PL
{
    class ConvertImagPathToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string imageRelatuveName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelatuveName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string imageRelatuveName = @"\pics\NoImag.jpg";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelatuveName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertColorToStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           OrderStatus status = (OrderStatus)value;
            if(status==OrderStatus.Delivered)
            {
                return Brushes.PaleGreen;
            }
            if (status == OrderStatus.Shipped) { return Brushes.CornflowerBlue; }
            else
            {
                return Brushes.LightPink;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertProg : IValueConverter
    {
        static readonly Random rand = new Random();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            

            OrderStatus orderStatus = (OrderStatus)value;
           switch(orderStatus)
            {
                case OrderStatus.Ordered:
                    return rand.Next(0,25);
                    case OrderStatus.Shipped:
                    return  rand.Next(25,50);
                    case OrderStatus.Delivered:
                    return rand.Next(50,100);
                    default:
                    return 0;
            }
               
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}