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
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IAccountHelper _accountHelper;
        public UserController(IUserHelper userHelper
                             , IAccountHelper accountHelper)
        {
            _userHelper = userHelper;
            _accountHelper = accountHelper;
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Read_Users()
        {
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

        [HttpGet]
        public IActionResult AddUser()
        {
            return PartialView("_AddUserPartial");
        }

        [HttpPost]
        public IActionResult AddUser(UserModel userModel)
        {
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
        [HttpGet]
        public IActionResult EditUser(Guid id)
        {
            return PartialView("_EditUserPartial", _userHelper.GetEditUserById(id));
        }
        [HttpPost]
        public IActionResult EditUser(UpdateUserModel userModel)
        {
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

        [HttpGet]
        public IActionResult DeleteUser(Guid id)
        {
            var userInfo = _userHelper.GetUserById(id);
            if (userInfo == null)
            {
                return BadRequest();
            }
            _userHelper.DeleteUser(id);
            return Ok(true);
        }
        [HttpGet]
        public IActionResult ViewUser(Guid id)
        {
            return PartialView("_ViewUserPartial", _userHelper.GetUserById(id));
        }

        public IActionResult GetUserStats()
        {
            var activeUsers = _userHelper.GetAllUsers().Count(u => u.IsActive == true);
            var inactiveUsers = _userHelper.GetAllUsers().Count(u => u.IsActive == false);
            return Json(new { activeUsers, inactiveUsers });
        }

    }
}
