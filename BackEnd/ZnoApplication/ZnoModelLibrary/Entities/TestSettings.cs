using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zno.DAL.Entities
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
        [Required]
        public Subject Subject { get; set; }

        /// <summary>
        /// Время тестирования в минутах
        /// </summary>
        [Required]
        public ushort TestingTime { get; set; }

        /// <summary>
        /// Количество вопросов каждой категории сложности
        /// </summary>
        [Required]
        public ushort NumberOfQuestions { get; set; }

        /// <summary>
        /// Список тестов из которых будут выбираться вопросы
        /// </summary>
        [Required]
        public IList<Test> Tests { get; set; }

        /// <summary>
        /// Категорий сложности вопросов которые будут выдаваться
        /// </summary>
        public IList<TestSettingsQuestionType> QuestionTypes { get; set; }

        public TestSettings()
        {
            QuestionTypes = new List<TestSettingsQuestionType>();
        }
    }
}