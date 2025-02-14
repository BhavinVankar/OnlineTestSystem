using Dapper;
using Microsoft.Extensions.Options;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.DataAccess.StoredProcedureDbAccess;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.Repository
{
    public class AssessmentRepository : SqlDbRepository<UserModel>, IAssessmentRepository
    {
        private readonly RepositoryOptions repositoryOptions;
        public AssessmentRepository(IOptions<RepositoryOptions> repositoryOptions) : base(repositoryOptions.Value.DefaultConnection)
        {
            this.repositoryOptions = repositoryOptions.Value;
        }

        public Guid AddAssessmentInfo(AssessmentModel assessmentData)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@TestName", assessmentData.TestName);
            vParams.Add("@Description", assessmentData.Description);
            vParams.Add("@PassingScore", assessmentData.PassingScore);
            vParams.Add("@CreatedOn", DateTime.UtcNow);
            vParams.Add("@IsActive", 1);
            vParams.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

            vconn.Execute("sp_proc_AddAssessmentInfo", vParams, commandType: CommandType.StoredProcedure);

            return vParams.Get<Guid>("@Id");
        }

        public void AddAssessmentMapping(AddAssessmentMappingModel addAssessmentMappingModel)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Id", addAssessmentMappingModel.Id);
            vParams.Add("@UserId", addAssessmentMappingModel.UserId);
            vParams.Add("@Assigned_At", DateTime.UtcNow);
            vParams.Add("@Status", 1);
            vParams.Add("@CreatedOn", DateTime.UtcNow);
            vconn.Execute("sp_proc_AddAssessmentMapping", vParams, commandType: CommandType.StoredProcedure);
        }

        public void AddQuestion(QuestionModel questionData)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@QuestionText", questionData.QuestionText);
            vParams.Add("@CorrectAnswer", questionData.CorrectAnswer);
            vParams.Add("@SectionId", questionData.SectionId);
            vParams.Add("@IsActive", 1);
            vParams.Add("@CreatedOn", DateTime.UtcNow);
            vconn.Execute("sp_proc_AddQuestion", vParams, commandType: CommandType.StoredProcedure);
        }
        public Guid AddSection(SectionModel sectionData)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@SectionName", sectionData.SectionName);
            vParams.Add("@TestId", sectionData.TestId);
            vParams.Add("@SectionOrder", sectionData.SectionOrder);
            vParams.Add("@IsActive", 1);
            vParams.Add("@CreatedOn", DateTime.UtcNow);
            vParams.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

            vconn.Execute("sp_proc_AddSection", vParams, commandType: CommandType.StoredProcedure);

            return vParams.Get<Guid>("@Id");
        }

        public void DeleteAssessment(Guid id)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Id", id);
            vParams.Add("@IsActive", 0);
            vParams.Add("@ModifiedOn", DateTime.UtcNow);
            vconn.Execute("sp_proc_DeleteAssessment", vParams, commandType: CommandType.StoredProcedure);

        }

        public List<AssessmentHistoryModel> GetAllAssessmentHistoryData(int statusId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@StatusId",statusId);
            var assessmentList = vconn.Query<AssessmentHistoryModel>("sp_proc_GetAllAssessmentHistoryDataByStatusId", vParams, commandType: CommandType.StoredProcedure);
            return assessmentList.ToList();

        }

        public List<AssessmentModel> GetAllAssessmentsData()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var assessmentList = vconn.Query<AssessmentModel>("sp_proc_GetAllAssessmentsData", vParams, commandType: CommandType.StoredProcedure);
            return assessmentList.ToList();
        }
        public List<AssessmentMappingModel> GetAllAssessmentsMappingData()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var assessmentList = vconn.Query<AssessmentMappingModel>("sp_proc_GetAllAssessmentsMappingData", vParams, commandType: CommandType.StoredProcedure);
            return assessmentList.ToList();
        }

        public List<AssessmentModel> GetAllPendingAssessmentsDataById(Guid userId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Id",userId);
            var assessmentList = vconn.Query<AssessmentModel>("sp_proc_GetAllPendingAssessmentsDataById", vParams, commandType: CommandType.StoredProcedure);
            return assessmentList.ToList();
        }

        public AssessmentResponseModel GetAssessmentById(Guid id)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@TestId", id);
            using var multi = vconn.QueryMultiple("sp_proc_GetAssessmentsById", vParams, commandType: CommandType.StoredProcedure);
            var assessment = multi.Read<AssessmentResponseModel>().FirstOrDefault();
            if (assessment == null) return null;
            var sections = multi.Read<SectionModel>().ToList();
            var questions = multi.Read<QuestionModel>().ToList();
            foreach (var section in sections)
            {
                section.Questions = questions.Where(q => q.SectionId == section.Id).ToList();
            }
            assessment.Sections = sections;
            return assessment;
        }

        public AssessmentMappingViewModel GetAssessmentMappingById(Guid id)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Id",id);
            var assessmentList = vconn.Query<AssessmentMappingViewModel>("sp_proc_GetAssessmentMappingById", vParams, commandType: CommandType.StoredProcedure);
            return assessmentList.FirstOrDefault();
        }

        public void SubmitAssessmentResult(AssessmentResultModel assessmentResultModel)
        {
            using var vconn = GetOpenConnection();

            var vParams = new DynamicParameters();
            vParams.Add("@Id", assessmentResultModel.Id);
            vParams.Add("@AssignmentId", assessmentResultModel.AssignmentId);
            vParams.Add("@Score", assessmentResultModel.Score);
            vParams.Add("@Status", assessmentResultModel.Status);
            vParams.Add("@CreatedOn", DateTime.UtcNow);
            vParams.Add("@CompletedOn", DateTime.UtcNow);
            vconn.Execute("sp_proc_SubmitAssessmentResult", vParams, commandType: CommandType.StoredProcedure);
        }

        public void UpdateAssessmentInfo(AssessmentRequestModel assessmentData)
        {
            using var vconn = GetOpenConnection();

            var vParams = new DynamicParameters();
            vParams.Add("@Id", assessmentData.Id);
            vParams.Add("@TestName", assessmentData.TestName);
            vParams.Add("@Description", assessmentData.Description);
            vParams.Add("@PassingScore", assessmentData.PassingScore);
            vParams.Add("@ModifiedOn", DateTime.UtcNow);
            vParams.Add("@IsActive", assessmentData.IsActive);

            vconn.Execute("sp_proc_UpdateAssessmentInfo", vParams, commandType: CommandType.StoredProcedure);

            // Update Sections
            if (assessmentData.Sections != null && assessmentData.Sections.Any())
            {
                foreach (var section in assessmentData.Sections)
                {
                    var sectionParams = new DynamicParameters();
                    sectionParams.Add("@SectionId", section.Id);
                    sectionParams.Add("@SectionName", section.SectionName);
                    sectionParams.Add("@TestId", assessmentData.Id);
                    sectionParams.Add("@SectionOrder", section.SectionOrder);
                    sectionParams.Add("@IsActive", section.IsActive);
                    sectionParams.Add("@ModifiedOn", DateTime.UtcNow);

                    vconn.Execute("sp_proc_UpdateSection", sectionParams, commandType: CommandType.StoredProcedure);

                    // Update Questions in this section
                    if (section.Questions != null && section.Questions.Any())
                    {
                        foreach (var question in section.Questions)
                        {
                            var questionParams = new DynamicParameters();
                            questionParams.Add("@QuestionId", question.Id);
                            questionParams.Add("@QuestionText", question.QuestionText);
                            questionParams.Add("@CorrectAnswer", question.CorrectAnswer);
                            questionParams.Add("@SectionId", section.Id);
                            questionParams.Add("@IsActive", question.IsActive);
                            questionParams.Add("@ModifiedOn", DateTime.UtcNow);

                            vconn.Execute("sp_proc_UpdateQuestion", questionParams, commandType: CommandType.StoredProcedure);
                        }
                    }
                }
            }
        }

    }
}
