using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Настройки для теста
    /// </summary>
    public class TestSettings
    {
        /// <summary>
        /// Индентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Предмет
        /// </summary>
        public Subject Subject { get; set; }

        /// <summary>
        /// Время тестирования в минутах
        /// </summary>
        public ushort TestingTime { get; set; }

        /// <summary>
        /// Количество вопросов каждой категории сложности
        /// </summary>
        public ushort NumberOfQuestions { get; set; }

        /// <summary>
        /// Список тестов из которых будут выбираться вопросы
        /// </summary>
        public IEnumerable<Test> Tests { get; set; }

        /// <summary>
        /// Категорий сложности вопросов которые будут выдаваться
        /// </summary>
        public IEnumerable<AnswerType> AnswerTypes { get; set; }
    }
}