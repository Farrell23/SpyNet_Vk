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
using System.Net;

using System.Windows.Controls.Primitives;
using System.Threading;
using System.ComponentModel;

namespace SpyNet_Vk
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class AuthorizationForm : UserControl
    {
        private MainWindow mainWindow;

        public AuthorizationForm()
        {
            InitializeComponent();

            if (!ConnectionAvailable("http://www.google.com"))
                VisibleToolTip(true);
            else
                VisibleToolTip(false);
        }

        /// <summary>
        /// Функционал по кнопке "Войти"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_Sign_vk_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectionAvailable("http://www.google.com"))
            {
                VisibleToolTip(true);
            }
            else
            {
                if (tB_login.Text.Trim() != "" & tB_password.Password.Trim() != "")  // если ввели и логин и пароль и соединение с интернетом есть
                {
                    text_hint.Visibility = Visibility.Hidden;
                    prog_view.Visibility = Visibility.Visible;

                    VisibleToolTip(false);

                    string email = tB_login.Text;
                    string password = tB_password.Password;

                    await Model.AuthAsync(email, password);

                    if (Model.vk.IsAuthorized)
                    {
                        DataAboutMyself data_myself = new DataAboutMyself();

                        var image = new BitmapImage(new Uri(Model.user_vk.Photo200.ToString()));
                        data_myself.shortAllInfo.main_picture.Source = image;
                        data_myself.shortAllInfo.main_info.Text = Model.ReceivingDataFromUser(Model.user_vk);

                        mainWindow.grid_frame.Children.Clear();
                        mainWindow.grid_frame.Children.Add(data_myself);

                        prog_view.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        NotificationAboutWrongData();
                    }
                }
                else
                {
                    NotificationAboutWrongData();
                }
            }
        }

        /// <summary>
        /// Увидомление про неправильный ввод данных
        /// </summary>
        public void NotificationAboutWrongData()
        {
            prog_view.Visibility = Visibility.Hidden;
            text_hint.Visibility = Visibility.Visible;
            text_hint.Visibility = Visibility.Hidden;
            text_hint.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Проверка на соединение с интернетом (True - есть интернет, false - скорее всего интернета нету)
        /// </summary>
        /// <param name="strServer"></param>
        /// <returns></returns>
        private bool ConnectionAvailable(string strServer)
        {
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(strServer);

                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Интернет безусловно есть! 
                    rspFP.Close();
                    return true;
                }
                else
                {
                    // сервер вернул отрицательный ответ, возможно что инета нет
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Window.GetWindow(this);
            if (!ConnectionAvailable("http://www.google.com"))
            {
                VisibleToolTip(true);
            }
        }

        /// <summary>
        /// Доступ к свойству Visibility 
        /// </summary>
        /// <param name="flag">True - показывает подсказку, false - скрывает ее</param>
        private void VisibleToolTip(bool flag)
        {
            if (flag)
                toolTip.Visibility = Visibility.Visible;
            else
                toolTip.Visibility = Visibility.Hidden;

            toolTip.Background = new LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90);
            toolTip.HasDropShadow = true;
            toolTip.Content = "Проверьте подключение к сети Интернет!";
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.grid_frame.Children.Clear();
            mainWindow.grid_frame.Children.Add(new SelectAct());
        }
    }
}