using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RussianTeaClubSite.Infrastructure.Abstract;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClubSite.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и пароль");

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