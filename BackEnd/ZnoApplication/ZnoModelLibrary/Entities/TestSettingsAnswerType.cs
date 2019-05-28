using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zno.DAL.Entities
{
    /// <summary>
    /// Промежуточная таблица между TestSettings и 
    /// QuestionType для связи многие ко многим
    /// </summary>
    public class TestSettingsQuestionType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TestSettingsId { get; set; }
        public TestSettings TestSettings { get; set; }

        public int QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}