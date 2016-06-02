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
using VkNet.Model.RequestParams;
using System.Threading;
using System.Windows;
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class GroupsModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список групп пользователя
        /// </summary>
        public ReadOnlyCollection<Group> groups { get; set; }

        /// <summary>
        /// Количество всех групп
        /// </summary>
        public int Length_groups { get; set; }

        /// <summary>
        /// Список id групп для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public GroupsModel()
        {
            Initialization = UpdateAsync();
        }


        #region Команды
        public ICommand Update_Groups
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    await UpdateAsync();
                });
            }
        }

        public ICommand Delete_SelectGroups
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteSelectGroups());
                    VisibilityProgressBar(false);
                    await Task.Factory.StartNew(() => Update());
                });
            }
        }

        public ICommand Delete_AllGroups
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteAllGroups());
                    VisibilityProgressBar(false);
                    await Task.Factory.StartNew(() => Update());
                });
            }
        }
        
        #endregion

        #region Функциональные методы

        /// <summary>
        /// Асинхронное обновление данных про группы
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            VisibilityProgressBar(true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(false);
        }

        /// <summary>
        /// Обновление данных о группах
        /// </summary>
        public void Update()
        {
            GroupsGetParams param = new GroupsGetParams();
            param.UserId = Model.vk.UserId;
            param.Extended = true;
            param.Filter = GroupsFilters.All;
            param.Fields = GroupsFields.All;

            groups = Model.vk.Groups.Get(param);

            Length_groups = groups.Count;

            OnPropertyChanged("groups");
            OnPropertyChanged("Length_groups");
        }
        /// <summary>
        /// Удаление выбраных групп
        /// </summary>
        private void DeleteSelectGroups()
        {
            list_id = GetListID(MyGroups.list);

            for (int i = 0; i < list_id.Count; i++)
            {
                Model.vk.Groups.Leave(list_id[i]);
                Thread.Sleep(2500);
            }
        }

        /// <summary>
        /// Удаление всех групп пользователя
        /// </summary>
        private void DeleteAllGroups()
        {
            for (int i = 0; i < groups.Count; i++)
            {
                Model.vk.Groups.Leave(groups[i].Id);
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
                list_id.Add(Convert.ToInt64(((Group)list[i]).Id.ToString()));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}