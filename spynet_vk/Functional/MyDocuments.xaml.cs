using System;
using System.Collections;
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

namespace SpyNet_Vk
{
    /// <summary>
    /// Логика взаимодействия для MyDocuments.xaml
    /// </summary>
    public partial class MyDocuments : UserControl
    {
        public static IList list;

        public MyDocuments()
        {
            InitializeComponent();
            list = listDocs.SelectedItems;
        }
    }      
}
