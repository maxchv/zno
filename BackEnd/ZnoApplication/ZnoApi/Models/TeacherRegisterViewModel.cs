using System.ComponentModel.DataAnnotations;

namespace Zno.Server.Models
{
    public class TeacherRegisterViewModel : RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
    }
}