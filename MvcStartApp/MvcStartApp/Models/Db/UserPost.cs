using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public class UserPost
    {
        /// <summary>
        /// Уникальный идентификатор поста пользователя в базе
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата появления
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Заголовок поста
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Id пользователя, добавившего пост
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Ссылка на пользователя, добавившего пост
        /// </summary>
        public User User { get; set; }

    }
}
