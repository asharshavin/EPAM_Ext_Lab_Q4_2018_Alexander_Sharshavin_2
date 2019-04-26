using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using MVC2Messenger.Models;
using DAL2Messenger; 

namespace MVC2Messenger.Controllers
{
    public class AccountController : Controller
    {
        private const string MessengerConnectionString = "MessengerConection";
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = repo.Users.GetAll().FirstOrDefault(u => u.name == model.Name && u.password == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = repo.Users.GetAll().FirstOrDefault(u => u.name == model.Name);

                if (user == null)
                {
                    repo.Users.Save(new User { name = model.Name, password = model.Password, role = 2 });

                    user = repo.Users.GetAll().FirstOrDefault(u => u.name == model.Name && u.password == model.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Chat");
        }
    }
}