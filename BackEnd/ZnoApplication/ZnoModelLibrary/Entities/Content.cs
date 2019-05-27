using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Модель "контент"
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        [Required]
        public ContentType Type { get; set; }

        /// <summary>
        /// Содержимое контента
        /// </summary>
        [Required]
        public string Data { get; set; }

        [Required]
        [JsonIgnore]
        public IList<QuestionContent> Questions { get; set; }

        public Content()
        {
            Questions = new List<QuestionContent>();
        }
    }
}