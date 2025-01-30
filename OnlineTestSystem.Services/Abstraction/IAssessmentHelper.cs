using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
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
        void DeleteAssessment(Guid id);
        List<AssessmentModel> GetAllAssessmentsData();
        AssessmentResponseModel GetAssessmentById(Guid id);
        void UpdateAssessmentInfo(AssessmentRequestModel assessmentRequestModel);
    }
}
