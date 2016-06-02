using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SpyNet_Vk.Converter
{
    public class DocPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new BitmapImage(new Uri(@"pack://application:,,,/SpyNet_Vk;component/Image/Document.png"));
            else
            {
                VkNet.Model.Attachments.Document doc = new VkNet.Model.Attachments.Document();
                doc.Preview = (VkNet.Model.Previews)value;
                return new BitmapImage(new Uri(doc.Preview.Photo.Sizes[0].Src.Value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
