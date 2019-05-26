using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Промежуточная таблица между Question и Content для
    /// связи многие ко многим
    /// </summary>
    public class QuestionContent
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}