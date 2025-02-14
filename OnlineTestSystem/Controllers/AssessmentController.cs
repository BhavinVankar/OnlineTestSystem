using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using OnlineTestSystem.Models.RequestModel;
using OnlineTestSystem.Models.ResponseModel;
using OnlineTestSystem.Services.Abstraction;
using OnlineTestSystem.Services.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineTestSystem.Controllers
{
    [Authorize]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentHelper _assessmentHelper;
        private static List<AssessmentRequestModel> testDatabase = new List<AssessmentRequestModel>();
        public AssessmentController(IAssessmentHelper assessmentHelper)
        {
            _assessmentHelper = assessmentHelper;
        }
        [HttpGet]
        public IActionResult AssessmentList()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Read_Assessments()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var assessmentsData = _assessmentHelper.GetAllAssessmentsData();
                if (assessmentsData == null)
                {
                    return BadRequest();
                }
                else
                {
                    var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserRole);
                    ViewBag.UserRole = userRoleClaim?.Value;
                    return PartialView("_AssessmentsList", assessmentsData);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public IActionResult AddAssessment()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return BadRequest();
                }
                return View(new AssessmentRequestModel());
            }
            catch (Exception)
            {
                return BadRequest(404);
            }
        }

        [HttpPost]
        public IActionResult SaveAssessment(AssessmentRequestModel assessmentRequestModel)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }

                var userIdclaims = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserId);
                ModelState.Clear();
                TryValidateModel(assessmentRequestModel);
                if (ModelState.IsValid)
                {
                    _assessmentHelper.AddAssessmentInfo(assessmentRequestModel);
                    return RedirectToAction("AddAssessment");
                }
                else
                {
                    return PartialView("AddAssessment", assessmentRequestModel);

                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult EditAssessment(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return BadRequest();
                }
                var assessmentInfo = _assessmentHelper.GetAssessmentById(id);
                if (assessmentInfo == null)
                {
                    return BadRequest();
                }
                return View(assessmentInfo);
            }
            catch (Exception)
            {
                return BadRequest(404);
            }
        }

        [HttpPost]
        public IActionResult UpdateAssessment(AssessmentRequestModel assessmentRequestModel)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }

                var userIdclaims = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserId);
                ModelState.Clear();
                TryValidateModel(assessmentRequestModel);
                if (ModelState.IsValid)
                {
                    _assessmentHelper.UpdateAssessmentInfo(assessmentRequestModel);
                    return RedirectToAction("AssessmentList");
                }
                else
                {
                    return PartialView("EditAssessment", assessmentRequestModel);

                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ViewAssessment(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var assessmentInfo = _assessmentHelper.GetAssessmentById(id);
                if (assessmentInfo == null)
                {
                    return BadRequest();
                }
                return PartialView("_ViewAssessmentPartial", assessmentInfo);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult DeleteAssessment(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var assessmentInfo = _assessmentHelper.GetAssessmentById(id);
                if (assessmentInfo == null)
                {
                    return BadRequest();
                }
                else
                {
                    _assessmentHelper.DeleteAssessment(id);
                    return RedirectToAction("AssessmentList", assessmentInfo);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult PendingAssessmentList()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Read_PendingAssessments()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserId);
                Guid userId = Guid.Parse(userIdClaim?.Value);
                var assessmentsData = _assessmentHelper.GetAllPendingAssessmentsDataById(userId);
                if (assessmentsData == null)
                {
                    return BadRequest();
                }
                else
                {
                    var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserRole);
                    ViewBag.UserRole = userRoleClaim?.Value;
                    return PartialView("_PendingAssessmentsList", assessmentsData);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public IActionResult TermsAnsConditionAssessment(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || !HttpContext.User.Claims.Any())
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var assessmentData = _assessmentHelper.GetAssessmentById(id);
                // Create terms and conditions list
                var termsAndConditions = new List<string>
                                        {
                                            "You must complete the assessment in one sitting.",
                                            "No external help is allowed during the assessment.",
                                            "Your progress will not be saved if you exit midway.",
                                            "The assessment has a fixed time limit.",
                                            "Any form of cheating will result in disqualification.",
                                            "Ensure a stable internet connection during the test."
                                        };

                // Pass terms and conditions to the view via ViewBag
                ViewBag.termsAndConditions = termsAndConditions;

                return PartialView("_TermsAndCondition", assessmentData);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult StartAssessment(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var assessmentData = _assessmentHelper.GetAssessmentById(id);
                return View(assessmentData);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpPost]
        public IActionResult SubmitAssessment(AssessmentResponseModel assessmentRequestModel)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }

                var userIdclaims = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserId);
                var userId=userIdclaims?.Value;
                ModelState.Clear();
                TryValidateModel(assessmentRequestModel);
                if (ModelState.IsValid)
                {
                    _assessmentHelper.SubmitAssessment(assessmentRequestModel, Guid.Parse(userId));
                    return Ok(true);
                }
                else
                {
                    return PartialView("AddAssessment", assessmentRequestModel);

                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AssessmentHistoryList()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Read_AssessmentHistory()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                int statusId = 2;
                var assessmentsData = _assessmentHelper.GetAllAssessmentHistoryData(statusId);
                if (assessmentsData == null)
                {
                    return BadRequest();
                }
                else
                {
                    var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserRole);
                    ViewBag.UserRole = userRoleClaim?.Value;
                    return PartialView("_AssessmentHistoryList", assessmentsData);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}
