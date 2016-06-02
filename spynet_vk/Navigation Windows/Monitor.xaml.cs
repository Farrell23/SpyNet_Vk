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

namespace SpyNet_Vk
{
    /// <summary>
    /// Логика взаимодействия для Monitor.xaml
    /// </summary>
    public partial class Monitor : UserControl
    {
        private MainWindow _mainWindow;

        public Monitor()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.grid_frame.Children.Clear();
            _mainWindow.grid_frame.Children.Add(new SelectAct());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
        }
    }
}
