using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
   public class SectionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TestId { get; set; }

        [Required]
        public string SectionName { get; set; }

        public int SectionOrder { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;

        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
    }
}
