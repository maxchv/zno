using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Модель "тест"
    /// </summary>
    public class Test
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ushort Year { get; set; }

        [Required]
        public TestType Type { get; set; }

        [Required]
        public Subject Subject { get; set; }
    }
}