using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ViewResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _authProvider.Signout();
                Session.Abandon();
                // clear authentication cookie
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
                {
                    Expires = DateTime.Now.AddYears(-1),
                    Path = FormsAuthentication.FormsCookiePath,
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);
                // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
                var cookie2 = new HttpCookie("ASP.NET_SessionId", "")
                {
                    Expires = DateTime.Now.AddYears(-1)
                };
                Response.Cookies.Add(cookie2);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(loginViewModel.UserName, loginViewModel.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}