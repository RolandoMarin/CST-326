using Microsoft.AspNetCore.Mvc;

namespace CST_326.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
