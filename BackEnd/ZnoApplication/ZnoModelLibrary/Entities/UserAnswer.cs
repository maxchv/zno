using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zno.DAL.Entities
{
    public class UserAnswer
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Question Question { get; set; }

        /// <summary>
        /// Ответы на вопрос
        /// </summary>
        public IList<Answer> Answers { get; set; }
    }
}