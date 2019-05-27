using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Модель "тест"
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        [Required]
        public ushort Year { get; set; }

        /// <summary>
        /// Тип теста
        /// </summary>
        [Required]
        public TestType Type { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        [Required]
        public Subject Subject { get; set; }
    }
}