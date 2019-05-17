using Microsoft.AspNetCore.Identity;

namespace ZnoModelLibrary.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Fio { get; set; }
    }
}