using CST_326.DAO;
using CST_326.Models;
using CST_326.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CST_326.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository userRepository;
        public AccountController()
        {
            userRepository = new UserRepository();
        }

        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult ProcessLogin(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            return View("Dashboard", userRepository.GetUser(user));
        }

        public IActionResult Register()
        {
            return View("Register");
        }



    }
}
