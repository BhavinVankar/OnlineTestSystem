using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using OnlineTestSystem.Services.Abstraction;

namespace OnlineTestSystem.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AssessmentMappingController : Controller
    {
        private readonly IAssessmentHelper _assessmentHelper;
        private readonly IUserHelper _userHelper;
        public AssessmentMappingController(IAssessmentHelper assessmentHelper, IUserHelper userHelper)
        {
            _assessmentHelper = assessmentHelper;
            _userHelper = userHelper;
        }
        [HttpGet]
        public IActionResult AssessmentMappingList()
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
        public IActionResult Read_AssessmentsMapping()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var assessmentsData = _assessmentHelper.GetAllAssessmentsMappingData();
                if (assessmentsData == null)
                {
                    return BadRequest();
                }
                else
                {
                    return PartialView("_AssessmentsMappingList", assessmentsData);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult AddAssessmentMapping()
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return Unauthorized();
                }

                var allUserData = _userHelper.GetAllUserData();
                var allAssessmentData = _assessmentHelper.GetAllAssessmentsData();

                if (allUserData == null || allAssessmentData == null)
                {
                    return NotFound("User or assessment data not found.");
                }

                ViewBag.Users = allUserData.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList();

                ViewBag.Assessments = allAssessmentData.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.TestName
                }).ToList();

                return PartialView("_AddAssessmentMappingPartial");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddAssessmentMapping: {ex.Message}");
                return StatusCode(500, "An internal error occurred.");
            }
        }
        [HttpPost]
        public IActionResult AddAssessmentMapping(AddAssessmentMappingModel addAssessmentMappingModel)
        {
            try
            {
                var listOfAssessment = _assessmentHelper.GetAllAssessmentsMappingData();

                // Convert the already assigned users to a HashSet for faster lookup
                var alreadyAssignedUsersSet = new HashSet<Guid>(
                    listOfAssessment
                    .Where(x => x.TestsId == addAssessmentMappingModel.Id)
                    .Select(x => x.UserId)
                );
                foreach (var item in addAssessmentMappingModel.users)
                {
                    if (alreadyAssignedUsersSet.Contains(Guid.Parse(item)))
                    {
                        continue;
                    }
                    else
                    {
                        addAssessmentMappingModel.UserId = Guid.Empty;
                        addAssessmentMappingModel.UserId = Guid.Parse(item);
                        _assessmentHelper.AddAssessmentMapping(addAssessmentMappingModel);
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult ViewAssessmentMapping(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var assessmentMappingInfo = _assessmentHelper.GetAssessmentMappingById(id);
                if (assessmentMappingInfo == null)
                {
                    return BadRequest();
                }
                return PartialView("_ViewAssessmentMappingPartial", assessmentMappingInfo);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


    }
}
