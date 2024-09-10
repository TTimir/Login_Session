using Login_Session.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Login_Session.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginSessionContext context;

        public HomeController(LoginSessionContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                ViewBag.userSession = HttpContext.Session.GetString("User").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            // to prevent login if user already logged in
            if (HttpContext.Session.GetString("User") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(TblUser user)
        {
            var myUser = context.TblUsers.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("User", myUser.Email);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Invalid Credential!";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                HttpContext.Session.Remove("User");
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TblUser user)
        {
            if (ModelState.IsValid)
            {
                await context.TblUsers.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully!";
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
