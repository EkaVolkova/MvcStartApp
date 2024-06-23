using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя в базе
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime JoinDate { get; set; }
    }
}
