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
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);//pn а как же Dependency Injection, которую мы проходили?

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
                var user = repo.Users.GetAll().FirstOrDefault(u => u.name == model.Name && u.password == model.Password);//pn не, так не пойдет. Надо отправлять логин/пароль в базу и пусть оттуда приходит одна запись. А то неоптимально получается. И вообще, почему у тебя логика фильтрации данных делает в UI (MVC - это UI)? Надо в DAL перенести.
				if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");//pn моё любимое: строки - в ресурсы :)
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
                    repo.Users.Save(new User { name = model.Name, password = model.Password, role = 2 });//pn хардкод. Роли (и все другие массивы констант) лучше как-то маппить из базы в enum-сущности, чтобы было проще работать с ними.

					user = repo.Users.GetAll().FirstOrDefault(u => u.name == model.Name && u.password == model.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Chat");
                }
				else //pn мне не нравится такая логика. Лучше сделать так: на 54й строке у тебя идет попытка сохранения пользователя. Пусть этот метод возвращает ещё и 2 поля: Error - bool, ErrorMessage - string, по которым ты сможешь уже в экшене определить, что делать. Сейчас ты размазываешь (почем зря) бизнес логику по UI, если есть возможность, лучше это делать в одном месте (в твоём случае - в БД)
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