using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SpyNet_Vk.Converter
{
    public class AudioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int min = 0, sec = 0;
            min = System.Convert.ToInt32(value) / 60;
            sec = System.Convert.ToInt32(value) - (min * 60);

            if (sec < 10)
               return min.ToString() + ":0" + sec.ToString();
            else
               return min.ToString() + ":" + sec.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
