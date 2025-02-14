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
        public int AssessmentId { get; set; }
        public int CandidateId { get; set; }
        public List<SectionSubmissionModel> Sections { get; set; }
    }

    public class SectionSubmissionModel
    {
        public int SectionId { get; set; }
        public List<QuestionSubmissionModel> Questions { get; set; }
    }

    public class QuestionSubmissionModel
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }


}
