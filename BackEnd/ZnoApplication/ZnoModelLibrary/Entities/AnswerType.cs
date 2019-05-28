using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zno.DAL.Entities
{
    /// <summary>
    /// Тип вопроса
    /// </summary>
    public class QuestionType
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
        public IList<TestSettingsQuestionType> TestSettings { get; set; }

        public QuestionType()
        {
            TestSettings = new List<TestSettingsQuestionType>();
        }
    }
}