using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SectionId { get; set; }

        [Required]
        public string QuestionText { get; set; }
        public int QuestionOrder { get; set; }
        // New property to store candidate’s selected answer
        public bool? SelectedAnswer { get; set; }
        public bool CorrectAnswer { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
