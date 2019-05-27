using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZnoModelLibrary.Entities
{
    /// <summary>
    /// Тип вопроса
    /// </summary>
    public class AnswerType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public IList<TestSettingsAnswerType> TestSettings { get; set; }

        public AnswerType()
        {
            TestSettings = new List<TestSettingsAnswerType>();
        }
    }
}