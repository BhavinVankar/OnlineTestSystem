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
    public class AccountController : Controller
    {
        private readonly IAccountHelper _accountHelper;
        public AccountController(IAccountHelper accountHelper)
        {
            _accountHelper = accountHelper;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
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
                            if (userInfo.Role == AppConstants.Admin && userInfo.IsActive == true && userInfo.IsDeleted == false)
                            {
                                string jToken = _accountHelper.GenerateToken(userInfo);
                                if (!string.IsNullOrEmpty(jToken))
                                {
                                    var claims = new List<Claim>()
                                        {
                                            new Claim(AppConstants.Token, jToken),
                                                new Claim(AppConstants.UserId, userInfo.UserId.ToString()),
                                                new Claim(AppConstants.UserRole, userInfo.Role)
                                            };
                                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                    var principal = new ClaimsPrincipal(identity);
                                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                                    {
                                        IsPersistent = true
                                    });

                                    HttpContext.Session.SetString(AppConstants.Token, jToken);
                                    userInfo.Token = jToken;
                                    return RedirectToAction("Dashboard", "User");
                                }
                            }
                            else
                            {
                                string jToken = _accountHelper.GenerateToken(userInfo);
                                if (!string.IsNullOrEmpty(jToken))
                                {
                                    var claims = new List<Claim>()
                                        {
                                                new Claim(AppConstants.Token, jToken),
                                                new Claim(AppConstants.UserId, userInfo.UserId.ToString()),
                                                new Claim(AppConstants.UserRole, userInfo.Role)
                                            };
                                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                    var principal = new ClaimsPrincipal(identity);
                                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                                    {
                                        IsPersistent = true
                                    });
                                    HttpContext.Session.SetString(AppConstants.Token, jToken);
                                    userInfo.Token = jToken;
                                    return RedirectToAction("Dashboard", "User");
                                }
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
        public IActionResult SignOut()
        {
            try
            {

                HttpContext.Session.Clear();
                Response.Cookies.Delete(AppConstants.Token);
                Response.Cookies.Delete(".AspNetCore.Session");
                HttpContext.SignOutAsync();
                return RedirectToAction("SignIn");
            }
            catch (Exception)
            {
                return BadRequest(404);
            }

        }
    }
}
