using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Модель "контент"
    /// </summary>
    public class Content
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ContentType Type { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public List<Question> Questions { get; set; }
    }
}