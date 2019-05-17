using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZnoModelLibrary.Entities
{
    public class GeneratedTest
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public List<Question> Questions { get; set; }
        public List<UserAnswer> Answers { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // Если true - тест не завершен. False - завершен
        public bool Status { get; set; } = true;
        public int CurrentPosition { get; set; } = 0;
        public int Score { get; set; }
    }
}
