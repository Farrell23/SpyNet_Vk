using SpyNet_Vk.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using VkNet.Model;
using VkNet.Model.RequestParams;

namespace SpyNet_Vk.ViewModel
{
    class VideoModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список групп пользователя
        /// </summary>
        public ReadOnlyCollection<VkNet.Model.Attachments.Video> video { get; set; }

        /// <summary>
        /// Количество всех групп
        /// </summary>
        public int Length_video { get; set; }

        /// <summary>
        /// Список id групп для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public VideoModel()
        {
            Update();
        }

        #region Команды

        public ICommand Update_Video
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

        public ICommand Delete_AllVideo
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteAllVideo());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public ICommand Delete_SelectVideo
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteSelectVideo());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public Task Initialization
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Функциональные методы

        /// <summary>
        /// Асинхронное обновление с прогресс-барами
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            VisibilityProgressBar(true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(false);
        }

        /// <summary>
        /// Не асинхронное обновление данных
        /// </summary>
        public void Update()
        {
            VideoGetParams param = new VideoGetParams();
            param.OwnerId = Model.vk.UserId;
            param.Count = 200;

            video = Model.vk.Video.Get(param);
            Length_video = video.Count;
            OnPropertyChanged("video");
            OnPropertyChanged("Length_video");
        }

        /// <summary>
        /// Удаление всех видеозаписей
        /// </summary>
        private void DeleteAllVideo()
        {
            bool mark = false;
            for (int i = 0; i < video.Count; i++)
            {
                mark = Model.vk.Video.Delete(video[i].Id.Value, Model.vk.UserId);
            }
            if (mark)
                System.Windows.Forms.MessageBox.Show("All video deleted!!!");
        }

        /// <summary>
        /// Удаление выбранных видеозаписей
        /// </summary>
        private void DeleteSelectVideo()
        {
            list_id = GetListID(MyVideo.list);

            for (int i = 0; i < list_id.Count; i++)
            {
                Model.vk.Video.Delete(list_id[i], Model.vk.UserId);
                Thread.Sleep(new Random().Next(100, 250));
            }
        }

        /// <summary>
        /// Получение списка индексов видеозаписей
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListID(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((VkNet.Model.Attachments.Video)list[i]).Id.ToString()));
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