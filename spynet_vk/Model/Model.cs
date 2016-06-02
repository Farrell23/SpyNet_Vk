using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;

namespace SpyNet_Vk
{
    static class Model
    {
        static private ulong appID = 5217616;    // ID приложения

        static public VkApi vk = new VkApi();
        static public User user_vk;  // все данные про авторизированного пользователя

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="email">Логин или номер телефона</param>
        /// <param name="password">Пароль</param>
        static public async Task AuthAsync(string email, string password)
        {
            await Task.Factory.StartNew(() => AuthorizationAsync(email, password));
            if(vk.IsAuthorized)
                GetUser();
        }

        /// <summary>
        /// Процесс авторизация
        /// </summary>
        /// <param name="email">Логин или номер телефона</param>
        /// <param name="password">Пароль</param>
        static private void AuthorizationAsync(string email, string password)
        {           
            try
            {
                VkNet.ApiAuthParams param = new ApiAuthParams();
                param.ApplicationId = appID;
                param.Login = email;
                param.Password = password;
                param.Settings = Settings.All;

                vk.Authorize(param);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Получаем данные о пользователе
        /// </summary>
        static private void GetUser()
        {
            user_vk = vk.Users.Get(vk.UserId.Value, ProfileFields.All);
        }

        /// <summary>
        /// Получение поверхтностной информации о пользователе 
        /// </summary>
        /// <param name="user">Пользователь про которого хотим узнать инфо</param>
        /// <returns></returns>
        static public string ReceivingDataFromUser(VkNet.Model.User user)
        {
            string data = "";
            data += " " + user.FirstName + "  " + user.LastName + Environment.NewLine;
            data += " ID пользователя - " + user.Id + Environment.NewLine;
            data += " ScreenName пользывателя - " + user.Domain + Environment.NewLine;
            data += " День Рождение - " + user.BirthDate + Environment.NewLine;
            data += " Мобильный телефон - " + user.MobilePhone + Environment.NewLine;

            data += Environment.NewLine;
            data += " Кол-во друзей - " + user.Counters.Friends + Environment.NewLine;
            data += " Кол-во подписчиков - " + user.Counters.Followers + Environment.NewLine;
            data += " Кол-во альбомов - " + user.Counters.Albums + Environment.NewLine;
            data += " Кол-во фотографий - " + user.Counters.Photos + Environment.NewLine;
            data += " Кол-во аудиозаписей - " + user.Counters.Audios + Environment.NewLine;
            data += " Кол-во видеозаписей - " + user.Counters.Videos + Environment.NewLine;
            data += " Кол-во заметок - " + user.Counters.Notes + Environment.NewLine;
            data += " Кол-во групп - " + user.Counters.Groups + Environment.NewLine;
            data += " Кол-во публичных страниц - " + user.Counters.Pages + Environment.NewLine;

            return data;
        }
    }
}