﻿using System;
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
    public partial class MyPhoto : UserControl
    {
        public static IList list_Albums;
        public static IList list_Photo;

        public MyPhoto()
        {
            InitializeComponent();

            list_Albums = listAlbums.SelectedItems;
            list_Photo = listPhoto.SelectedItems;
        }
    }      
}
