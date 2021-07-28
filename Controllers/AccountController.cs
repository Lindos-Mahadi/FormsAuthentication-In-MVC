using FormsAuthentication_In_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FormsAuthentication_In_MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MemberShip ship)
        {
            using (var context = new FormsAutenticationEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == ship.UserName && x.Password == ship.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(ship.UserName, false);
                    return RedirectToAction("Index","Employees");
                }
                ModelState.AddModelError("","Invalid UserName and Password");
                return View();
            }
            
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User userModel)
        {
            using (var context = new FormsAutenticationEntities())
            {
                context.User.Add(userModel);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}