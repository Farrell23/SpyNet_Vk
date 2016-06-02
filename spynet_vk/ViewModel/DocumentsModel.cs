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
using System.Windows;
using System.Net;
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class DocumentsModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Список данных про все документы пользователя
        /// </summary>
        public ReadOnlyCollection<VkNet.Model.Attachments.Document> docs { get; set; }

        /// <summary>
        /// Список id документов для удаления
        /// </summary>
        public List<long> list_id;

        /// <summary>
        /// Количество всех документов
        /// </summary>
        public int Length_docs { get; set; }

        /// <summary>
        /// Показатель видимости прогрес-бара
        /// </summary>
        public Visibility EnableProgress { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public DocumentsModel()
        {
            Initialization = UpdateAsync();
        }

        #region Команды
        public ICommand Update_Docs
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    await UpdateAsync();
                });
            }
        }

        public ICommand Delete_SelectDocs
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DeleteSelectDocs());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(false);
                });
            }
        }

        public ICommand Delete_AllDocs
        {
            get
            {
                return new ActionCommand(async () =>
               {
                   VisibilityProgressBar(true);
                   await Task.Factory.StartNew(() => DeleteAllDocs());
                   await Task.Factory.StartNew(() => Update());
                   VisibilityProgressBar(false);
               });
            }
        }

        public ICommand DownloadDoc
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(true);
                    await Task.Factory.StartNew(() => DownloadDocument());
                    VisibilityProgressBar(false);
                });
            }
        }

        #endregion

        #region Функциональные методы

        /// <summary>
        /// Асинхронное обновление данных о документах
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            VisibilityProgressBar(true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(false);
        }

        /// <summary>
        /// Обновление данных о документах
        /// </summary>
        public void Update()
        {
            docs = Model.vk.Docs.Get();
            Length_docs = docs.Count;

            OnPropertyChanged("docs");
            OnPropertyChanged("Length_docs");
        }

        /// <summary>
        /// Удаление выбраных документов
        /// </summary>
        private void DeleteSelectDocs()
        {
            list_id = GetListID(MyDocuments.list);

            for (int i = 0; i < list_id.Count; i++)
            {
                long index = list_id[i];
                Model.vk.Docs.Delete(Model.vk.UserId.Value, index);
            }
        }

        /// <summary>
        /// Удаление всех документов
        /// </summary>
        private void DeleteAllDocs()
        {
            for (int i = 0; i < docs.Count; i++)
            {
                Model.vk.Docs.Delete(Model.vk.UserId.Value, docs[i].Id.Value);
            }
        }

        /// <summary>
        /// Скачивает выбранные документы
        /// </summary>
        private void DownloadDocument()
        {
            for (int i = 0; i < MyDocuments.list.Count; i++)
            {
                VkNet.Model.Attachments.Document doc = (VkNet.Model.Attachments.Document)MyDocuments.list[i];
                new WebClient().DownloadFile(doc.Url, doc.Title);
            }
        }

        /// <summary>
        /// Получение списка индексов документов
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListID(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((VkNet.Model.Attachments.Document)list[i]).Id.ToString()));
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