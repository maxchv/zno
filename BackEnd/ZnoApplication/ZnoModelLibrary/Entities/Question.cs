using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zno.DAL.Entities;

namespace Zno.DAL.Entities
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
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Тест к которому относиться данный вопрос
        /// </summary>
        [Required]
        public Test Test { get; set; }

        /// <summary>
        /// Содержимое вопроса
        /// </summary>
        [Required]
        public IList<Answer> Answers { get; set; }

        /// <summary>
        /// Тип контента вопроса
        /// </summary>
        public ContentType ContentType { get; set; }

        /// <summary>
        /// Содержимое вопроса 
        /// </summary>
        public string Content { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}