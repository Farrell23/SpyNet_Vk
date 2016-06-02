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
    /// Interaction logic for Data_About_Myself.xaml
    /// </summary>
    public partial class DataAboutMyself : UserControl
    {
        private MainWindow mainWindow;
        public ShortInformation shortAllInfo = new ShortInformation();

        public DataAboutMyself()
        {
            InitializeComponent();
            ShowInfoUser.Children.Add(shortAllInfo);
        }
        
        #region Разделы в StackPanel
        private void btn_main_Click(object sender, RoutedEventArgs e)
        {            
            var image = new BitmapImage(new Uri(Model.user_vk.Photo200.ToString()));
            shortAllInfo.main_picture.Source = image;
            shortAllInfo.main_info.Text = Model.ReceivingDataFromUser(Model.user_vk);

            ShowInfoUser.Children.Clear();
            ShowInfoUser.Children.Add(shortAllInfo);
        }

        private void btn_edit_friends_Click(object sender, RoutedEventArgs e)
        {
            MyFriends myFriends = new MyFriends();
            ShowInfoUser.Children.Add(myFriends);
        }

        private void btn_edit_message_Click(object sender, RoutedEventArgs e)
        {
            MyMessages myMessages = new MyMessages();
            ShowInfoUser.Children.Add(myMessages);
        }

        private void btn_edit_photo_Click(object sender, RoutedEventArgs e)
        {
            MyPhoto myPhoto = new MyPhoto();
            ShowInfoUser.Children.Add(myPhoto);
        }

        private void btn_edit_audio_Click(object sender, RoutedEventArgs e)
        {
            MyAudio myAudio = new MyAudio();
            ShowInfoUser.Children.Add(myAudio);
        }

        private void btn_edit_video_Click(object sender, RoutedEventArgs e)
        {
            MyVideo myVideo = new MyVideo();
            ShowInfoUser.Children.Add(myVideo);
        }

        private void btn_edit_groups_Click(object sender, RoutedEventArgs e)
        {
            MyGroups myGroup = new MyGroups();
            ShowInfoUser.Children.Add(myGroup);
        }

        private void btn_edit_app_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_edit_docs_Click(object sender, RoutedEventArgs e)
        {
            MyDocuments myDocs = new MyDocuments();
            ShowInfoUser.Children.Add(myDocs);
        }

        private void btn_edit_wall_Click(object sender, RoutedEventArgs e)
        {
            MyWall myWall = new MyWall();
            ShowInfoUser.Children.Add(myWall);
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Window.GetWindow(this);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.grid_frame.Children.Clear();

            AuthorizationForm auth_form = new AuthorizationForm();
            auth_form.tB_login.Text = String.Empty;
            auth_form.tB_password.Password = String.Empty;

            mainWindow.grid_frame.Children.Add(auth_form);
        }
    }
}