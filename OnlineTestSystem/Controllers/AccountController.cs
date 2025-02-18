using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using OnlineTestSystem.Services.Abstraction;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace OnlineTestSystem.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        private readonly IAccountHelper _accountHelper;
        public AccountController(IAccountHelper accountHelper)
        {
            _accountHelper = accountHelper;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            if (HttpContext.User.Claims.Any() || HttpContext.User.Claims.Count() > 0)
            {
                return RedirectToAction("Dashboard", "User");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(SignInModel loginModel)
        {
            try
            {
                TryValidateModel(loginModel);
                if (ModelState.IsValid)
                {
                    var IsEmailExists = _accountHelper.CheckEmailExists(loginModel.EmailAddress);
                    if (IsEmailExists)
                    {
                        var userInfo = _accountHelper.SignIn(loginModel);
                        if (userInfo != null)
                        {
                            if (userInfo.IsActive == true && userInfo.IsDeleted == false)
                            {
                                var claims = new List<Claim>()
                                        {
                                                new Claim(AppConstants.UserId, userInfo.Id.ToString()),
                                                new Claim(AppConstants.UserRole, userInfo.Role)
                                            };
                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var principal = new ClaimsPrincipal(identity);
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                                {
                                    IsPersistent = true
                                });

                                return RedirectToAction("Dashboard", "User");
                            }
                        }
                    }
                    ModelState.AddModelError("Password", "Invalid Email Address or Password!");
                    return View(loginModel);
                }
                ModelState.AddModelError("Password", "Invalid Email Address or Password!");
                return View(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            try
            {
                return SignOut();
            }
            catch (Exception)
            {
                return BadRequest(404);
            }

        }
    }
}
