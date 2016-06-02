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
using System.Threading;

using System.Windows;
using SpyNet_Vk.Interface;

namespace SpyNet_Vk.ViewModel
{
    class PhotoModel : IAsyncUpdate, INotifyPropertyChanged
    {
        /// <summary>
        /// Количество всех альбомов
        /// </summary>
        public ReadOnlyCollection<PhotoAlbum> Albums { get; set; }

        /// <summary>
        /// Альбом для привязки (показ фото из альбома)
        /// </summary>
        public ReadOnlyCollection<VkNet.Model.Attachments.Photo> PhotoForBinding { get; set; }

        /// <summary>
        /// Список id альбомов и альбомов для удаления
        /// </summary>
        public List<long> list_id;

        public List<long> list_idAlbums;
        public List<long> list_idPhoto;

        private int length_albums;
        private int length_photo;

        long SelectIndexAlbum;

        public int Length_albums
        {
            get { return length_albums; }
            set
            {
                length_albums = Albums.Count;
                OnPropertyChanged("length_albums");
            }
        }

        public int Length_photo
        {
            get { return length_photo; }
            set
            {
                length_photo = PhotoForBinding.Count;
                OnPropertyChanged("Length_photo");
            }
        }

        public Visibility EnableProgressPhoto { get; set; } = Visibility.Hidden;

        public Visibility EnableProgressAlbums { get; set; } = Visibility.Hidden;

        public Task Initialization { get; private set; }

        public PhotoModel()
        {
            SelectIndexAlbum = -1;

            Initialization = UpdateAsync();
        }

        #region Команды
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

        public ICommand Delete_AllAlbums
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(EnableProgressAlbums, true);
                    await Task.Factory.StartNew(() => DeleteAllAlbums());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(EnableProgressAlbums, false);
                });
            }
        }

        public ICommand Delete_SelectAlbum
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(EnableProgressAlbums, true);
                    await Task.Factory.StartNew(() => DeleteSelectAlbum());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(EnableProgressAlbums, false);
                });
            }
        }

        public ICommand Delete_AllPhoto
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(EnableProgressPhoto, true);
                    await Task.Factory.StartNew(() => DeleteAllPhoto());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(EnableProgressPhoto, false);
                });
            }
        }

        public ICommand Delete_SelectPhoto
        {
            get
            {
                return new ActionCommand(async () =>
                {
                    VisibilityProgressBar(EnableProgressPhoto, true);
                    await Task.Factory.StartNew(() => DeleteSelectPhoto());
                    await Task.Factory.StartNew(() => Update());
                    VisibilityProgressBar(EnableProgressPhoto, false);
                });
            }
        }

        public ICommand Select_Album
        {
            get
            {
                return new ActionCommand(() =>
                {
                    list_idAlbums = GetListIDAlbum(MyPhoto.list_Albums);
                    SelectIndexAlbum = Convert.ToInt64(list_idAlbums[0]);

                    switch (SelectIndexAlbum)
                    {
                        case -6:
                            PhotoForBinding = Get_SystemPhotoAlbum(PhotoAlbumType.Profile);
                            break;
                        case -7:
                            PhotoForBinding = Get_SystemPhotoAlbum(PhotoAlbumType.Wall);
                            break;
                        case -15:
                            PhotoForBinding = Get_SystemPhotoAlbum(PhotoAlbumType.Saved);
                            break;
                        default:
                            PhotoForBinding = Get_PhotoInAlbum(SelectIndexAlbum);
                            break;
                    }
                    OnPropertyChanged("Photo_for_binding");
                    length_photo = PhotoForBinding.Count;
                    OnPropertyChanged("Length_photo");
                });
            }
        }

        #endregion

        #region Функциональные методы

        public async Task UpdateAsync()
        {
            VisibilityProgressBar(EnableProgressAlbums, true);
            await Task.Factory.StartNew(() => Update());
            VisibilityProgressBar(EnableProgressAlbums, false);
        }

        /// <summary>
        /// Обновление данных о фотографиях и альбомах пользователя
        /// </summary>
        public void Update()
        {
            UpdateGet_AllAlbums();
            UpdateGet_PhotoInAlbums();
        }

        /// <summary>
        /// Удаление всех альбомов кроме системных
        /// </summary>
        private void DeleteAllAlbums()
        {
            bool mark = false;
            for (int i = 0; i < Albums.Count; i++)
            {
                if (CheckSystemAlbums(Albums[i].Id))
                {
                    mark = Model.vk.Photo.DeleteAlbum(Albums[i].Id);
                }
                else
                {
                    DeletePhotoInProfileAlbum();
                    DeletePhotoInSavedAlbum();
                    DeletePhotoInWallAlbum();

                    //DeletePhotoInSystemAlbum(PhotoAlbumType.Profile);
                    //DeletePhotoInSystemAlbum(PhotoAlbumType.Saved);
                    //DeletePhotoInSystemAlbum(PhotoAlbumType.Wall);
                }
            }
            if (mark)
                System.Windows.Forms.MessageBox.Show("All Albums delete");
        }

        /// <summary>
        /// Удаление всех фотографий в системном альбоме, после альбом остается пустым
        /// </summary>
        /// <param name="photo">Список фотографий в системном альбоме </param>
        private void DeleteAllPhotoInOneSystemAlbum(ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo)
        {
            for (int i = 0; i < photo.Count; i++)
            {
                Model.vk.Photo.Delete(Convert.ToUInt64(photo[i].Id.Value));
            }
        }

        /// <summary>
        /// Проверка на принадлежание альбома к системным
        /// </summary>
        /// <param name="id">Id альбома</param>
        /// <returns>True - альбом НЕ системный False - являеться системным альбомом </returns>
        private bool CheckSystemAlbums(long id)
        {
            switch (id)
            {
                case -6:
                case -7:
                case -15:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Удаление всех фотографий в альбоме "Аватары"
        /// </summary>
        private void DeletePhotoInProfileAlbum()
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;
            photo = Get_SystemPhotoAlbum(PhotoAlbumType.Profile);
            DeleteAllPhotoInOneSystemAlbum(photo);
        }

        /// <summary>
        /// Удаление всех фотографий в альбоме "Сохраненные фотографии"
        /// </summary>
        private void DeletePhotoInSavedAlbum()
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;
            photo = Get_SystemPhotoAlbum(PhotoAlbumType.Saved);
            DeleteAllPhotoInOneSystemAlbum(photo);
        }

        /// <summary>
        /// Удаление всех фотографий в альбоме "Фотографии со стены"
        /// </summary>
        private void DeletePhotoInWallAlbum()
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;
            photo = Get_SystemPhotoAlbum(PhotoAlbumType.Wall);
            DeleteAllPhotoInOneSystemAlbum(photo);
        }

        // ---------------------------------------------------------------------

        private void DeletePhotoInSystemAlbum(PhotoAlbumType type)
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;
            photo = Get_SystemPhotoAlbum(type);
            DeleteAllPhotoInOneSystemAlbum(photo);
        }

        // ---------------------------------------------------------------------

        /// <summary>
        /// Удаление выбраных альбомов
        /// </summary>
        private void DeleteSelectAlbum()
        {
            list_idAlbums = GetListIDAlbum(MyPhoto.list_Albums);
            bool mark = false;

            for (int i = 0; i < list_idAlbums.Count; i++)
            {
                if (CheckSystemAlbums(list_idAlbums[i]))
                    mark = Model.vk.Photo.DeleteAlbum(Convert.ToInt64(list_idAlbums[i]));
            }

            if (mark)
                System.Windows.Forms.MessageBox.Show("Albums delete");
            SelectIndexAlbum = -1;
        }

        /// <summary>
        /// Удаление всех фото в выбранном альбоме
        /// </summary>
        private void DeleteAllPhoto()
        {
            for (int i = 0; i < PhotoForBinding.Count; i++)
            {
                Model.vk.Photo.Delete(Convert.ToUInt32(PhotoForBinding[i].Id.Value));
            }
            System.Windows.Forms.MessageBox.Show("All photo deleted!");
        }

        /// <summary>
        /// Удаление выбранных фото в выбраном альбоме
        /// </summary>
        private void DeleteSelectPhoto()
        {
            list_idPhoto = GetListIDPhoto(MyPhoto.list_Photo);

            for (int i = 0; i < list_idPhoto.Count; i++)
            {
                Model.vk.Photo.Delete(Convert.ToUInt64(list_idPhoto[i]));
            }
        }

        /// <summary>
        /// Обновление всех альбомов
        /// </summary>
        public void UpdateGet_AllAlbums()
        {
            int AlbumsCount;    // Кол-во альбомов всего, вместе с "системными"

            PhotoGetAlbumsParams paramAlbum = new PhotoGetAlbumsParams();
            paramAlbum.OwnerId = Model.vk.UserId.Value;
            paramAlbum.NeedSystem = true;
            paramAlbum.NeedCovers = true;

            Albums = Model.vk.Photo.GetAlbums(out AlbumsCount, paramAlbum);
            length_albums = Albums.Count;
            OnPropertyChanged("Albums");
            OnPropertyChanged("Length_albums");
        }

        private void UpdateGet_PhotoInAlbums()
        {
            if (list_idPhoto != null && list_idPhoto.Count != 0)
            {
                PhotoForBinding = Get_PhotoInAlbum(SelectIndexAlbum); // index_Albums
                OnPropertyChanged("Photo_for_binding");

                length_photo = PhotoForBinding.Count;
                OnPropertyChanged("Length_photo");

                // UpdatePhotoInAlbums();
            }
            else if (SelectIndexAlbum != -1)
                UpdatePhotoInAlbums();
        }

        /// <summary>
        /// Обновление фото в заданом альбоме
        /// </summary>
        private void UpdatePhotoInAlbums()
        {
            PhotoForBinding = Get_PhotoInAlbum(SelectIndexAlbum);
            OnPropertyChanged("Photo_for_binding");

            length_photo = PhotoForBinding.Count;
            OnPropertyChanged("Length_photo");
        }

        /// <summary>
        /// Получение списка фотографий из альбома
        /// </summary>
        /// <param name="numder">Id альбома</param>
        /// <returns></returns>
        public ReadOnlyCollection<VkNet.Model.Attachments.Photo> Get_PhotoInAlbum(long numder)
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;   // список фотографий в альбоме

            PhotoGetParams param = new PhotoGetParams();
            param.AlbumId = PhotoAlbumType.Id(numder);

            int count = 0;
            photo = Model.vk.Photo.Get(out count, param);

            return photo;
        }

        /// <summary>
        /// Получение фотографий из системных альбомов
        /// </summary>
        /// <param name="Photo_type">Индификатор альбома (Аватары, Сохраненные фотографии или Фотографии со стены)</param>
        /// <returns></returns>
        public ReadOnlyCollection<VkNet.Model.Attachments.Photo> Get_SystemPhotoAlbum(PhotoAlbumType Photo_type)
        {
            ReadOnlyCollection<VkNet.Model.Attachments.Photo> photo;   // список фотографий в альбоме

            int CountPhotoInAlbum = 0;
            PhotoGetParams param = new PhotoGetParams();
            param.OwnerId = Model.vk.UserId.Value;
            param.AlbumId = Photo_type;
            param.Reversed = true;

            photo = Model.vk.Photo.Get(out CountPhotoInAlbum, param);

            return photo;
        }

        /// <summary>
        /// Получение списка индексов альбомов
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListIDAlbum(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((PhotoAlbum)list[i]).Id.ToString()));
            }
            return list_id;
        }

        /// <summary>
        /// Получение списка индексов фотографий
        /// </summary>
        /// <param name="list">Список выбраных индексов</param>
        /// <returns></returns>
        private List<long> GetListIDPhoto(System.Collections.IList list)
        {
            list_id = new List<long>();
            for (int i = 0; i < list.Count; i++)
            {
                list_id.Add(Convert.ToInt64(((VkNet.Model.Attachments.Photo)list[i]).Id.ToString()));
            }
            return list_id;
        }

        /// <summary>
        /// Работа с видимостью прогрес-бара
        /// </summary>
        /// <param name="EnableProgress">Cвойство для показа прогресс-бара</param>
        /// <param name="mark">True - виден прогрес-бар, False - скрыт прогрес-бар</param>
        private void VisibilityProgressBar(Visibility EnableProgress, bool mark)
        {
            if (mark)
            {
                EnableProgress = Visibility.Visible;
            }
            else
                EnableProgress = Visibility.Hidden;

            OnPropertyChanged("EnableProgressPhoto");
            OnPropertyChanged("EnableProgressAlbums");
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