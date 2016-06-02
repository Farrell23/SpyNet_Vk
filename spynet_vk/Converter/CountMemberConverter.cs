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
    public class CountMemberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int countMember = System.Convert.ToInt32(value);

            int millions = 0;
            int thousands = 0;
            int hundreds = 0;

            if (countMember >= 1000 & countMember <= 9999)
            {
                thousands = countMember / 1000;
                hundreds = countMember - (thousands * 1000);
                return String.Format("{0} {1}", thousands, hundreds);
            }
            else if (countMember >= 10000 & countMember <= 99999)
            {
                thousands = countMember / 1000;
                hundreds = countMember - (thousands * 1000);
                return String.Format("{0} {1}", thousands, hundreds);
            }
            else if (countMember >= 100000 & countMember <= 999999)
            {
                thousands = countMember / 1000;
                hundreds = countMember - (thousands * 1000);
                return String.Format("{0} {1}", thousands, hundreds);
            }
            else if (countMember >= 1000000 & countMember <= 9999999)
            {
                millions = countMember / 1000000;
                thousands = (countMember - (millions * 1000000)) / 1000;
                hundreds = countMember - millions * 1000000 - thousands * 1000;

                if(thousands >= 10 & thousands <= 99)
                    return String.Format("{0} 0{1} {2}", millions, thousands, hundreds);
                else if(thousands >= 0 & thousands <= 9)
                    return String.Format("{0} 00{1} {2}", millions, thousands, hundreds);
                else
                    return String.Format("{0} {1} {2}", millions, thousands, hundreds);
            }
            else
                return String.Format("{0}", countMember);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
