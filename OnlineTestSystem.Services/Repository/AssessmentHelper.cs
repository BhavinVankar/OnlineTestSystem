using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Repository
{
    public class AssessmentHelper : IAssessmentHelper
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;
        public AssessmentHelper(IAssessmentRepository assessmentRepository,IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;
        }

        public void AddAssessmentInfo(AssessmentRequestModel assessmentRequestModel)
        {
           
            var assessmentData = _mapper.Map<AssessmentRequestModel,AssessmentModel>(assessmentRequestModel);
            Guid testId = _assessmentRepository.AddAssessmentInfo(assessmentData);
            int counter=1;
            foreach (var item in assessmentRequestModel.Sections)
            {
                item.TestId = testId;
                item.SectionOrder = counter;
                //Add section info and also get section id to add in question 
                Guid sectionId = _assessmentRepository.AddSection(item);
                foreach (var questionInfo in item.Questions)
                {
                    //add question 
                    questionInfo.SectionId = sectionId;
                    questionInfo.QuestionOrder = counter;
                    _assessmentRepository.AddQuestion(questionInfo);
                }
                counter++;
            }
        }
        public void UpdateAssessmentInfo(AssessmentRequestModel assessmentRequestModel)
        {

             _assessmentRepository.UpdateAssessmentInfo(assessmentRequestModel);
        }
        public void DeleteAssessment(Guid id)
        {
            _assessmentRepository.DeleteAssessment(id);
        }

        public List<AssessmentModel> GetAllAssessmentsData()
        {
            var allAssessmentList = _assessmentRepository.GetAllAssessmentsData();
            if (allAssessmentList != null)
            {
                return allAssessmentList;
            }
            else
            {
                return null;
            }
        }

        public AssessmentResponseModel GetAssessmentById(Guid id)
        {
            var assessmentInfo=_assessmentRepository.GetAssessmentById(id);
            return assessmentInfo;
        }
    }
}
