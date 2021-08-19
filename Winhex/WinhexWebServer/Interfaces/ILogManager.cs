using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WinhexWebServer.Models;

namespace WinhexWebServer.Interfaces
{
    /// <summary>
    /// Модель для действий с логами
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// Добавить логи для пользователя 
        /// </summary>
        /// <param name="log">Объект логов</param>
        /// <returns></returns>
        bool AddUserLog(UserLog log);
        /// <summary>
        /// Получить всех доступных пользователей
        /// </summary>
        /// <returns></returns>
        UserLog[] Users { get; }
        /// <summary>
        /// Получить логи конкретного пользователя
        /// </summary>
        /// <param name="act">Предикат для выбора пользователя</param>
        /// <returns></returns>
        UserLog GetUserLog(Expression<Func<UserLog, bool>> act);
        /// <summary>
        /// Установить кличку для конкретного пользователя  
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="note">Кличка</param>
        /// <returns></returns>
        bool SetNote(int id, string note);
        /// <summary>
        /// Ключ для получения действий пользователя
        /// </summary>
        string UserKey { get; }
    }
}