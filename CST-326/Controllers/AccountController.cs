using CST_326.Business;
using CST_326.DAO;
using CST_326.Models;
using CST_326.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySqlX.XDevAPI;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace CST_326.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository userRepository;
        private UserBusiness createUser = new UserBusiness();
        public AccountController()
        {
            userRepository = new UserRepository();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ProcessLoginAsync(LoginViewModel model)
        {
            var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, model.UserName)
                };

            var identity = new ClaimsIdentity(claims, "MyAuthenticationScheme");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            if (ModelState.IsValid)
            {
                var user = userRepository.GetUser(createUser.GetUser(model));
                if (user != null)
                {
                    HttpContext.Session.SetString("UserModel", JsonSerializer.Serialize(user));
                    return RedirectToAction("Home");
                }
            }
            return View("Login", model);

        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registeredUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = userRepository.CreateUser(createUser.AddUser(registeredUser));

            if (user)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Email Address is already taken");
                return View(registeredUser);
            }

        }
        [Authorize]
        public IActionResult Home()
        {
            var userModel = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("UserModel"));
            return View("Dashboard",userModel);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");  // Redirect to login or home page
        }

        public IActionResult ViewAccounts()
        {
            var userModel = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("UserModel"));

            // Retrieve accounts associated with the user
            var userAccounts = userRepository.GetAccounts(userModel);

            // Create a tuple containing the user object and the associated accounts
            var model = new Tuple<User, List<Account>>(userModel, userAccounts);

            // Pass the tuple to the view
            return View("ViewAccounts", model);
        }

    }





}
