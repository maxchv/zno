using System.ComponentModel.DataAnnotations;

namespace Zno.Server.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^\+?(38)?(0\d{9})")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} and no more {1}", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}