using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineTestSystem.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
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
    }
}
