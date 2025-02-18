using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using OnlineTestSystem.Services.Abstraction;
using OnlineTestSystem.Services.Repository;
using System;

namespace OnlineTestSystem.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IAccountHelper _accountHelper;
        public UserController(IUserHelper userHelper, IAccountHelper accountHelper)
        {
            _userHelper = userHelper;
            _accountHelper = accountHelper;
        }
        public IActionResult Dashboard()
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
        public IActionResult UserList()
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
        public IActionResult Read_Users()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var usersData = _userHelper.GetAllUserData();
                if (usersData == null)
                {
                    return BadRequest();
                }
                else
                {
                    var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserRole);
                    ViewBag.UserRole = userRoleClaim?.Value;
                    return PartialView("_UserList", usersData);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return BadRequest();
                }
                return PartialView("_AddUserPartial");
            }
            catch (Exception e)
            {
                return BadRequest(404);
            }
        }

        [HttpPost]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }

                var userIdclaims = User.Claims.FirstOrDefault(c => c.Type == AppConstants.UserId);
                ModelState.Clear();
                TryValidateModel(userModel);
                if (ModelState.IsValid)
                {
                    var emailExists = _accountHelper.CheckEmailExists(userModel.EmailAddress);
                    if (emailExists)
                    {
                        ModelState.AddModelError("EmailAddress", "Email Address is Exists");
                        return PartialView("_AddUserPartial", userModel);
                    }
                    _userHelper.AddUser(userModel);
                    return Ok(true);
                }
                else
                {
                    return PartialView("_AddUserPartial", userModel);

                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult EditUser(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var userInfo = _userHelper.GetEditUserById(id);
                if (userInfo == null)
                {
                    return BadRequest();
                }
                return PartialView("_EditUserPartial", userInfo);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult EditUser(UpdateUserModel userModel)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                ModelState.Clear();
                TryValidateModel(userModel);
                userModel.Role = AppConstants.Candidate;
                if (userModel.EmailAddress != null)
                {
                    var emailExists = _accountHelper.CheckEmailExistsByUserId(userModel.Role, userModel.EmailAddress, userModel.Id);
                    if (emailExists)
                    {
                        ModelState.AddModelError("EmailAddress", "Email Address is Exists");
                    }
                }
                if (ModelState.IsValid)
                {
                    _userHelper.UpdateUser(userModel);
                    return Ok(true);
                }
                return PartialView("_EditUserPartial", userModel);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var userInfo = _userHelper.GetUserById(id);
                if (userInfo == null)
                {
                    return BadRequest();
                }
                _userHelper.DeleteUser(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult ViewUser(Guid id)
        {
            try
            {
                if (HttpContext.User.Claims == null || HttpContext.User.Claims.Count() == 0)
                {
                    return Unauthorized();
                }
                var userInfo = _userHelper.GetUserById(id);
                if (userInfo == null)
                {
                    return BadRequest();
                }
                return PartialView("_ViewUserPartial", userInfo);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult GetUserStats()
        {
            var activeUsers = _userHelper.GetAllUsers().Count(u => u.IsActive == true);
            var inactiveUsers = _userHelper.GetAllUsers().Count(u => u.IsActive == false);
            return Json(new { activeUsers, inactiveUsers });
        }

    }
}
