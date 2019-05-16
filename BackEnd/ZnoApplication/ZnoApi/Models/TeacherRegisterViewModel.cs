using System.ComponentModel.DataAnnotations;

namespace ZnoApi.Models
{
    public class TeacherRegisterViewModel : RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
    }
}