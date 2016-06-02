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

namespace SpyNet_Vk
{
    /// <summary>
    /// Логика взаимодействия для SelectAct.xaml
    /// </summary>
    public partial class SelectAct : UserControl
    {
        private MainWindow _mainWindow;

        public SelectAct()
        {
            InitializeComponent();
        }

        private void btn_monitor_Click(object sender, RoutedEventArgs e)
        {
            Monitor monitor = new Monitor();
            _mainWindow.grid_frame.Children.Clear();
            _mainWindow.grid_frame.Children.Add(monitor);
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationForm authorization_user = new AuthorizationForm();      
            _mainWindow.grid_frame.Children.Clear();
            _mainWindow.grid_frame.Children.Add(authorization_user);
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindow = (MainWindow)Window.GetWindow(this);
        }
    }
}