using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zno.DAL.Entities
{
    /// <summary>
    /// Ответ на вопрос
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Идентификатор
        /// </summary>

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Тип ответа
        /// </summary>
        public ContentType ContentType { get; set; }

        /// <summary>
        /// Контент
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Правильный ответ или нет?
        /// </summary>
        public bool RightAnswer { get; set; }

        /// <summary>
        /// Вопрос к которому относится ответ
        /// </summary>
        public Question Question { get; set; }
    }
}
