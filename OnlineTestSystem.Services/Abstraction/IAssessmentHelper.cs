using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Abstraction
{
    public interface IAssessmentHelper
    {
        void AddAssessmentInfo(AssessmentRequestModel assessmentRequestModel);
        void AddAssessmentMapping(AddAssessmentMappingModel addAssessmentMappingModel);
        void DeleteAssessment(Guid id);
        List<AssessmentModel> GetAllAssessmentsData();
        List<AssessmentMappingModel> GetAllAssessmentsMappingData();
        List<AssessmentModel> GetAllPendingAssessmentsDataById(Guid userId);
        AssessmentResponseModel GetAssessmentById(Guid id);
        AssessmentMappingViewModel GetAssessmentMappingById(Guid id);
        void UpdateAssessmentInfo(AssessmentRequestModel assessmentRequestModel);
    }
}
