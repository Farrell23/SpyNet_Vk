using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class MessageModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список сообщений и диалогов пользователя
        /// </summary>
        public ReadOnlyCollection<Message> message { get; set; }

        /// <summary>
        /// Количество всех диалогов
        /// </summary>
        public int Length_dialogs { get; set; }

        /// <summary>
        /// Список id диалогов для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public MessageModel()
        {
            Initialization = UpdateAsync();
        }

        #region Комманды

        public ICommand UpdateData
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    await UpdateAsync();
                });
            }
        }

        public ICommand DeleteAllDialogs
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => Delete_AllDialogs());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public ICommand DeleteSelectDialogs
        {
            get
            {
                return new ActionCommand(async() =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => Delete_SelectDialogs());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        #endregion

        #region Функциональные методы

        /// <summary>
        /// Асинхронная инициализация/обновление
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            VisibilityProgressBar(true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(false);
        }

        /// <summary>
        /// Обновление данных про диалоги и беседы
        /// </summary>
        public void Update()
        {
            MessagesDialogsGetParams param = new MessagesDialogsGetParams();
            param.Count = 200;
            param.PreviewLength = 50;

            //MessagesGetObject mess1 = Model.vk.Messages.get(param);]

            message = Model.vk.Messages.GetDialogs(param).Messages;
            Length_dialogs = message.Count;

            OnPropertyChanged("message");
            OnPropertyChanged("Length_dialogs");
        }

        /// <summary>
        /// Удаление всех диалогов и бесед
        /// </summary>
        private void Delete_AllDialogs()
        {
            bool mark = false;
            for (int i = 0; i < message.Count; i++)
            {
                if (message[1].UsersCount > 1)
                    Model.vk.Messages.DeleteDialog(message[i].ChatId.Value, true);  // Признак удаляются ли сообщения из беседы (true)
                else
                    Model.vk.Messages.DeleteDialog(message[i].UserId.Value, false);  // Признак удаляются ли сообщения из диалога с указанным пользователем (false).
                mark = true;
            }
            if (mark)
                System.Windows.Forms.MessageBox.Show("All Dialogs deleted!!!");
        }

        private int GetID(long id)
        {
            for (int i = 0; i < message.Count; i++)
            {
                if (message[i].Id == id)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Удаление выбранных диалогов
        /// </summary>
        private void Delete_SelectDialogs()
        {
            bool mark = false;
            list_id = GetListID(MyMessages.list);
            
            int index = GetID(list_id[0]);

            for (int i = 0; i < list_id.Count; i++)
            {
                mark = Model.vk.Messages.DeleteDialog(Model.vk.UserId.Value, false, count:10000);
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
                list_id.Add(Convert.ToInt64(((Message)list[i]).Id.ToString()));
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