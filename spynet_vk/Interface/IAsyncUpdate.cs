using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyNet_Vk.Interface
{
   public interface IAsyncUpdate
    {
        /// <summary>
        /// Результат асинхронной инициализации
        /// </summary>
        Task Initialization { get; }

        /// <summary>
        /// Асинхронная инициализация/обновление
        /// </summary>
        Task UpdateAsync();

        /// <summary>
        /// Основная не асинхронная логика обновления данных
        /// </summary>
        void Update();
    }
}
