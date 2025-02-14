using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Models.ResponseModel;
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
        void AddAssessmentMapping(AddAssessmentMappingModel addAssessmentMappingModel);
        void AddQuestion(QuestionModel questionData);
        Guid AddSection(SectionModel sectionData);
        void DeleteAssessment(Guid id);
        List<AssessmentHistoryModel> GetAllAssessmentHistoryData(int statusId);
        List<AssessmentModel> GetAllAssessmentsData();
        List<AssessmentMappingModel> GetAllAssessmentsMappingData();
        List<AssessmentModel> GetAllPendingAssessmentsDataById(Guid userId);
        AssessmentResponseModel GetAssessmentById(Guid id);
        AssessmentMappingViewModel GetAssessmentMappingById(Guid id);
        void SubmitAssessmentResult(AssessmentResultModel assessmentResultModel);
        void UpdateAssessmentInfo(AssessmentRequestModel assessmentData);
    }
}
