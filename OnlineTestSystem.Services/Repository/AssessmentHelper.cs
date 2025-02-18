using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Models.ResponseModel;
using OnlineTestSystem.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OnlineTestSystem.Models.Common.Enums;

namespace OnlineTestSystem.Services.Repository
{
    public class AssessmentHelper : IAssessmentHelper
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;
        public AssessmentHelper(IAssessmentRepository assessmentRepository, IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;
        }

        public void AddAssessmentInfo(AssessmentRequestModel assessmentRequestModel)
        {

            var assessmentData = _mapper.Map<AssessmentRequestModel, AssessmentModel>(assessmentRequestModel);
            Guid testId = _assessmentRepository.AddAssessmentInfo(assessmentData);
            int counter = 1;
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
                return new List<AssessmentModel>();
            }
        }
        public AssessmentResponseModel GetAssessmentById(Guid id)
        {
            var assessmentInfo = _assessmentRepository.GetAssessmentById(id);
            return assessmentInfo;
        }
        public List<AssessmentMappingModel> GetAllAssessmentsMappingData()
        {
            var allAssessmentMappingList = _assessmentRepository.GetAllAssessmentsMappingData();
            if (allAssessmentMappingList != null)
            {
                return allAssessmentMappingList;
            }
            else
            {
                return new List<AssessmentMappingModel>();
            }
        }
        public void AddAssessmentMapping(AddAssessmentMappingModel addAssessmentMappingModel)
        {
            _assessmentRepository.AddAssessmentMapping(addAssessmentMappingModel);
        }
        public AssessmentMappingViewModel GetAssessmentMappingById(Guid id)
        {
            var result = _assessmentRepository.GetAssessmentMappingById(id);
            if (result == null)
            {
                return new AssessmentMappingViewModel();
            }
            else
            {
                return result;
            }
        }
        public List<AssessmentModel> GetAllPendingAssessmentsDataById(Guid userId)
        {
            var allPendingAssessment = _assessmentRepository.GetAllPendingAssessmentsDataById(userId);
            return allPendingAssessment;
        }
        public void SubmitAssessment(AssessmentResponseModel assessmentRequestModel, Guid userId)
        {
            AssessmentResultModel assessmentResultModel = new();
            int correctAnswers = 0;
            foreach (var section in assessmentRequestModel.Sections)
            {
                var candidateSection = assessmentRequestModel.Sections.FirstOrDefault(s => s.Id == section.Id);
                if (candidateSection != null)
                {
                    foreach (var question in section.Questions)
                    {
                        var candidateQuestion = candidateSection.Questions.FirstOrDefault(q => q.Id == question.Id);
                        if (candidateQuestion != null)
                        {
                            if (candidateQuestion.SelectedAnswer == question.CorrectAnswer)
                            {
                                correctAnswers++;
                            }
                        }
                    }
                }
            }
            //Correct Answer Stored in Passing Score 
            assessmentRequestModel.PassingScore = correctAnswers;
            var assessmentInfo = _assessmentRepository.GetAssessmentById(assessmentRequestModel.Id);


            var totalQuestions = assessmentRequestModel.Sections.Sum(s => s.Questions.Count());
            // Calculate the percentage score if there are questions
            if (totalQuestions > 0)
            {
                assessmentResultModel.Score = (decimal)correctAnswers / totalQuestions * 100;
                if (assessmentResultModel.Score >= assessmentInfo.PassingScore)
                {
                    assessmentResultModel.Status = Convert.ToInt32(AssessmentStatus.Pass);
                }
                else
                {
                    assessmentResultModel.Status = Convert.ToInt32(AssessmentStatus.Fail);
                }
            }
            var getAssessmentMappingData = _assessmentRepository.GetAllAssessmentsMappingData().Where(x => x.TestsId == assessmentRequestModel.Id && x.UserId == userId).FirstOrDefault();
            assessmentResultModel.AssignmentId = getAssessmentMappingData.Id;
            _assessmentRepository.SubmitAssessmentResult(assessmentResultModel);
            //_assessmentRepository.SubmitAssessment(assessmentRequestModel);
        }
        public List<AssessmentHistoryModel> GetAllAssessmentHistoryData(int statusId)
        {
            return _assessmentRepository.GetAllAssessmentHistoryData(statusId);
        }
    }
}
