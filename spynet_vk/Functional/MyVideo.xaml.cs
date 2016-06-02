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
    /// Логика взаимодействия для MyPhoto.xaml
    /// </summary>
    public partial class MyVideo : UserControl
    {
        public static IList list;

        public MyVideo()
        {
            InitializeComponent();
            list = list_video.SelectedItems;
        }

        //private string Duration(int time)
        //{
        //    string duration = "";

        //    int min = 0, sec = 0;
        //    min = time / 60;
        //    sec = time - (min * 60);

        //    if (sec < 10)
        //        duration = min.ToString() + ":0" + sec.ToString();
        //    else
        //        duration = min.ToString() + ":" + sec.ToString();

        //    return duration;
        //}
        
    }      
}