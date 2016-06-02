using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using System.Collections;
using System.Windows;
using VkNet.Enums;
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class FriendsModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список друзей пользователей
        /// </summary>
        public ReadOnlyCollection<User> friends { get; set; }

        /// <summary>
        /// Список пользователей подписчиков
        /// </summary>
        public ReadOnlyCollection<User> followers { get; set; }

        /// <summary>
        /// Список id групп для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Количество всех друзей
        /// </summary>
        public int Length_friends { get; set; }

        /// <summary>
        /// Количество всех подписчиков
        /// </summary>
        public int Length_followers { get; set; }

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public FriendsModel()
        {
            Initialization = UpdateAsync();
        }

        #region Команды
        public ICommand Update_Friends
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    await UpdateAsync();
                });
            }
        }

        public ICommand Delete_SelectFriends
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteSelectFriends());
                    VisibilityProgressBar(false);
                    await Task.Factory.StartNew(() => Update());
                });
            }
        }

        public ICommand Delete_AllFriends
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteAllFriends());
                    VisibilityProgressBar(false);
                    await Task.Factory.StartNew(() => Update());
                });
            }
        }
        #endregion

        #region Функциональные методы

        public async Task UpdateAsync()
        {
            VisibilityProgressBar(true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(false);
        }

        /// <summary>
        /// Обновление данных о друзьях и подписчиков
        /// </summary>
        public void Update()
        {
            FriendsGetParams param = new FriendsGetParams();
            param.UserId = Model.vk.UserId.Value;
            param.Fields = ProfileFields.All;
            param.Order = FriendsOrder.Hints;

            friends = Model.vk.Friends.Get(param);
            Length_friends = friends.Count;
            OnPropertyChanged("friends");
            OnPropertyChanged("Length_friends");

            followers = Model.vk.Users.GetFollowers(Model.vk.UserId, 1000, 0, ProfileFields.All);  // Model.user_vk.Counters.Followers
            Length_followers = followers.Count;
            OnPropertyChanged("followers");
            OnPropertyChanged("Length_followers");
        }

        /// <summary>
        /// Удаление выбраных друзей
        /// </summary>
        private void DeleteSelectFriends()
        {
            list_id = GetListID(MyFriends.list);

            for (int i = 0; i < list_id.Count; i++)
            {
                DeleteFriendStatus ss = Model.vk.Friends.Delete(list_id[i]);
            }
        }

        /// <summary>
        /// Удаление всех друзей
        /// </summary>
        private void DeleteAllFriends()
        {
            for (int i = 0; i < friends.Count; i++)
            {
                Model.vk.Friends.Delete(friends[i].Id);
                System.Threading.Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Получение списка индексов групп
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListID(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((User)list[i]).Id.ToString()));
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

        public void request()
        {
            IDictionary<long, ReadOnlyCollection<long>> request;

            FriendsGetRequestsParams param = new FriendsGetRequestsParams();
            param.Out = true;
            param.Extended = true;
            param.Count = 500;
            param.Sort = true;

            request = Model.vk.Friends.GetRequests(param);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}