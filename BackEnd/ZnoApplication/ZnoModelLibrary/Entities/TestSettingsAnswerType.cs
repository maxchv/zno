using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Промежуточная таблица между TestSettings и 
    /// AnswerType для связи многие ко многим
    /// </summary>
    public class TestSettingsAnswerType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TestSettingsId { get; set; }
        public TestSettings TestSettings { get; set; }

        public int AnswerTypeId { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}