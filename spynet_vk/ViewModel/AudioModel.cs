using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class AudioModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список всех аудиозаписей пользователя
        /// </summary>
        public ReadOnlyCollection<VkNet.Model.Attachments.Audio> audio { get; set; }

        /// <summary>
        /// Количество всех аудиозаписей
        /// </summary>
        public int Length_audio { get; set; }

        /// <summary>
        /// Список id audio для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public AudioModel()
        {
            Initialization = UpdateAsync();
        }

        #region Команды

        public ICommand Update_Audio
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    await UpdateAsync();
                });
            }
        }

        public ICommand Delete_SelectAudio
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteSelectAudio());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public ICommand Delete_AllAudio
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteAllAudio());
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
        /// Обновление данных про аудиозаписи пользователя
        /// </summary>
        public void Update()
        {
            AudioGetParams param = new AudioGetParams();
            param.OwnerId = Model.vk.UserId.Value;

            User user;
            audio = Model.vk.Audio.Get(out user, param);
            Length_audio = audio.Count;

            OnPropertyChanged("audio");
            OnPropertyChanged("Length_audio");
        }

        /// <summary>
        /// Удаление выбранных аудиозаписей
        /// </summary>
        private void DeleteSelectAudio()
        {
            list_id = GetListID(MyAudio.list);

            for (int i = 0; i < list_id.Count; i++)
            {
                Model.vk.Audio.Delete(list_id[i], Model.vk.UserId.Value);
            }
        }

        /// <summary>
        /// Удаление всех аудиозаписей
        /// </summary>
        private void DeleteAllAudio()
        {
            bool mark = false;
            for (int i = 0; i < audio.Count; i++)
            {
                mark = Model.vk.Audio.Delete(audio[i].Id.Value, Model.vk.UserId.Value);
            }
            if (mark)
                System.Windows.Forms.MessageBox.Show("All Audio deleted!!!");
            else
                System.Windows.Forms.MessageBox.Show("Error!!!");
        }

        /// <summary>
        /// Получение списка индексов аудиозаписей
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListID(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((VkNet.Model.Attachments.Audio)list[i]).Id.ToString()));
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}