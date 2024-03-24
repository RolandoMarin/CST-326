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
            return View();
        }

        [HttpPost]
        public IActionResult ProcessRegistration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            // Create an instance of UserRepository
            var userRepository = new UserRepository();

            // Call RegisterUser method on the userRepository instance
            userRepository.RegisterUser(model);

            // Redirect to login page or any other page as needed
            return RedirectToAction("Login");
        }


    }
}
