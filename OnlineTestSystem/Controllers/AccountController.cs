using Microsoft.AspNetCore.Mvc;
using OnlineTestSystem.Models;
using OnlineTestSystem.Services.Abstraction;

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
        public IActionResult SignIn(SignInModel signInModel)
        {
            var roleInfo = _accountHelper.GetUserDetails(signInModel);
            if(roleInfo != null)
            {
                if(roleInfo.Role == "Admin")
                {

                }
                else
                {

                }
            }
            return View();
        }
    }
}
