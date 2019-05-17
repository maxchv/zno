using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Модель "вопрос"
    /// </summary>
    public class Question
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public AnswerType AnswerType { get; set; }

        [Required]
        public Test Test { get; set; }

        [Required]
        public string AnswerJson { get; set; }

        [Required]
        public List<Content> Contents { get; set; }
    }
}