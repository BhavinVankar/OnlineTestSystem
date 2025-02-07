using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models.RequestModel
{
    public class AssessmentSubmissionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string TestName { get; set; }
        public List<SectionViewModel> Sections { get; set; }
    }

    public class SectionViewModel
    {
        public string SectionName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }

    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }

}
