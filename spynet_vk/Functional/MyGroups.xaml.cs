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

using System.Collections.ObjectModel;
using System.Collections;

namespace SpyNet_Vk
{
    /// <summary>
    /// Логика взаимодействия для MyGroups.xaml
    /// </summary>
    public partial class MyGroups : UserControl
    {
        public static IList list;

        public MyGroups()
        {
            InitializeComponent();
            list = listGroups.SelectedItems;
        }
    }
}
