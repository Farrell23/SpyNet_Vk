using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using System.Collections.ObjectModel;
using System.Windows;

namespace SpyNet_Vk.ViewModel
{
    class WallModel : INotifyPropertyChanged
    {
        public ReadOnlyCollection<Post> post { get; set; }

        public ulong Length_wall { get; set; }

        /// <summary>
        /// Список id записей на стене для удаления
        /// </summary>
        public List<long> list_id;


        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public WallModel()
        {
            Update();           
        }

        #region Комманды

        public ICommand UpdateData
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public ICommand DeleteSelectPost
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => Delete_SelectPost());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);

                });
            }
        }

        public ICommand DeleteAllPost
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => Delete_AllPost());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);

                });
            }
        }

        #endregion

        #region Функциональные методы

        /// <summary>
        /// Обновление данных о записях на стене пользователя
        /// </summary>
        private void Update()
        {
            WallGetParams param = new WallGetParams();
            param.OwnerId = Model.vk.UserId;
            param.Filter = WallFilter.All;
            param.Extended = true;

            WallGetObject wall = Model.vk.Wall.Get(param);
            Length_wall = wall.TotalCount;
            post = wall.WallPosts;

            OnPropertyChanged("post");
            OnPropertyChanged("Length_wall");
        }

        /// <summary>
        /// Удаляет все записи со стены пользователя 
        /// </summary>
        private void Delete_AllPost()
        {
            for (int i = 0; i < post.Count; i++)
            {
                bool mark = Model.vk.Wall.Delete(Model.vk.UserId, post[i].Id);
            }
        }

        /// <summary>
        /// Удаляет выбраные записи со стены
        /// </summary>
        private void Delete_SelectPost()
        {
            list_id = GetListID(MyWall.list);
            for (int i = 0; i < list_id.Count; i++)
            {
                bool mark = Model.vk.Wall.Delete(Model.vk.UserId, list_id[i]);
            }
        }

        private List<long> GetListID(System.Collections.IList list)
        {
            List<long> list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((Post)list[i]).Id.ToString()));
            }
            return list_id;
        }

        /// <summary>
        /// Работа с видимостью прогрес-бара
        /// </summary>
        /// <param name="mark">True - виден прогрес-бар, False - скрыт прогрес-бар</param>
        private void VisibilityProgressBar(bool mark)
        {
            if (mark)
            {
                EnableProgress = Visibility.Visible;
            }
            else
                EnableProgress = Visibility.Hidden;

            OnPropertyChanged("EnableProgress");
        }
        #endregion

        //public void Delete_AllWall()
        //{
        //    bool mark = false;
        //    for (int i = 0; i < Convert.ToInt32(Wall.TotalCount); i++)
        //    {
        //        mark = Model.vk.Wall.Delete(Model.vk.UserId.Value, Wall.WallPosts[i].Id);
        //    }
        //    if (mark)
        //        System.Windows.Forms.MessageBox.Show("All post from your wall deleted!!!");
        //}




        //private void Update_Information()
        //{
        //    Load_Update();
        //    list_wall.Items.Clear();
        //    for (int i = 0; i < Convert.ToInt32(data_about_user.wall.TotalCount); i++)
        //    {
        //        StackPanel stack_attachments = new StackPanel();

        //        StackPanel stack = new StackPanel();
        //        stack.Orientation = Orientation.Horizontal;
        //        stack.Tag = i.ToString();

        //        Label label = new Label();
        //        label.Foreground = Brushes.Black;
        //        Image image = new Image();
        //        image.Height = image.Width = 50;

        //        if (data_about_user.wall.WallPosts[i].Text != "")
        //        {
        //            long id = data_about_user.wall.WallPosts[i].FromId.Value;
        //            VkNet.Model.User user_post = data_about_user.vk.Users.Get(id, VkNet.Enums.Filters.ProfileFields.Photo100);

        //            image.Source = new BitmapImage(new Uri(user_post.Photo100.ToString()));
        //            label.Content = user_post.FirstName + "  " + user_post.LastName + Environment.NewLine +
        //                             data_about_user.wall.WallPosts[i].Text + Environment.NewLine +
        //                           "Опубликованно - " + data_about_user.wall.WallPosts[i].Date;
        //            stack.Children.Add(image);
        //            stack.Children.Add(label);
        //        }
        //        else
        //        {
        //            VkNet.Model.Group group = data_about_user.vk.Groups.GetById(data_about_user.wall.WallPosts[i].CopyHistory[0].FromId.Value/-1, VkNet.Enums.Filters.GroupsFields.All);
        //            image.Source = new BitmapImage(new Uri(group.Photo100.ToString()));
        //            label.Content = group.Name + Environment.NewLine +
        //                            data_about_user.wall.WallPosts[i].CopyHistory[0].Date + Environment.NewLine +
        //                            data_about_user.wall.WallPosts[i].CopyHistory[0].Text;
        //            stack.Children.Add(image);
        //            stack.Children.Add(label);

        //            for (int j = 0; j < data_about_user.wall.WallPosts[i].CopyHistory[0].Attachments.Count; j++)
        //            {
        //                Label im = new Label();
        //                im.Content = data_about_user.wall.WallPosts[i].CopyHistory[0].Attachments[j].Instance.ToString();
        //                //Image im = new Image();
        //                //im.Source = new BitmapImage(new Uri(data_about_user.wall.WallPosts[i].Attachments[j].Instance.ToString()));
        //                stack_attachments.Children.Add(im);
        //            }
        //            stack.Children.Add(stack_attachments);
        //            Label time = new Label();
        //            time.Content = data_about_user.wall.WallPosts[i].Date;
        //            stack.Children.Add(time);
        //        }
        //        list_wall.Items.Add(stack);
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}