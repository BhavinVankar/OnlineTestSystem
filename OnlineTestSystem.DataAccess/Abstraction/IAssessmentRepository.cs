using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.Abstraction
{
    public interface IAssessmentRepository
    {
        Guid AddAssessmentInfo(AssessmentModel assessmentData);
        void AddQuestion(QuestionModel questionData);
        Guid AddSection(SectionModel sectionData);
        void DeleteAssessment(Guid id);
        List<AssessmentModel> GetAllAssessmentsData();
        AssessmentResponseModel GetAssessmentById(Guid id);
        void UpdateAssessmentInfo(AssessmentRequestModel assessmentData);
    }
}
