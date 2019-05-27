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
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        [Required]
        public AnswerType AnswerType { get; set; }

        /// <summary>
        /// Тест к которому относиться данный вопрос
        /// </summary>
        [Required]
        public Test Test { get; set; }

        /// <summary>
        /// Ответ в формате JSON
        /// </summary>
        [Required]
        public string AnswerJson { get; set; }

        /// <summary>
        /// Содержимое вопроса
        /// </summary>
        [Required]
        public IList<QuestionContent> Contents { get; set; }

        public Question()
        {
            Contents = new List<QuestionContent>();
        }
    }
}