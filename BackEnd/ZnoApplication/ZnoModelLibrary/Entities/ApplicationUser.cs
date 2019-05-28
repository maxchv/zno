using Microsoft.AspNetCore.Identity;

namespace Zno.DAL.Entities
{
    /// <summary>
    ///  Модель "пользователь"
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Статус (в сети, не в сети, в тестировании)
        /// </summary>
        public Status Status { get; set; }
    }
}