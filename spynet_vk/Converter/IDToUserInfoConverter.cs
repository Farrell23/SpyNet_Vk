using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace SpyNet_Vk.Converter
{
    /// <summary>
    /// Конвертация с ID в Имя и Фамилию пользователя
    /// </summary>
    public class IDToUserName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long ID = System.Convert.ToInt64(value);
            User user = Model.vk.Users.Get(ID, ProfileFields.FirstName | ProfileFields.LastName);

            return String.Format("{0} {1}", user.FirstName, user.LastName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// Конвертация с ID в фото пользователя
    /// </summary>
    public class IDToUserPhoto : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long ID = System.Convert.ToInt64(value);
            User user = Model.vk.Users.Get(ID, ProfileFields.Photo50);

            return user.Photo50; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// Конвертация с ID в значение в онлайне ли пользователь
    /// </summary>
    public class IDToUserIsOnline : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long ID = System.Convert.ToInt64(value);
            User user = Model.vk.Users.Get(ID, ProfileFields.Online);

            if (user.Online.Value)
                return "Online";
            else
                return "Offline";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
