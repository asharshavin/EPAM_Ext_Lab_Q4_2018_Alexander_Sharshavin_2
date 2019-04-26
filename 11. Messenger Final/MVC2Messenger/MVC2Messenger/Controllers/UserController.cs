using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC2Messenger.Models;
using DAL2Messenger;

namespace MVC2Messenger.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private const string MessengerConnectionString = "MessengerConection";
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public static UserModel MapDAL2Model(User objectDAL)
        {
            return new UserModel
            {
                UserId = objectDAL.id,
                Username = objectDAL.name,
                RoleId = objectDAL.role,
                Role = RoleController.MapDAL2Model(repo.Roles.Get(objectDAL.role))
            };
        }

        public static User MapModel2DAL(UserModel objectModel)
        {
            return new User
            {
                id = objectModel.UserId,
                name = objectModel.Username,
                role = objectModel.RoleId
            };
        }

        public ActionResult Index()
        {
            var users = repo.Users.GetAll();
            var userModels = users.Select(x => MapDAL2Model(x));

            return View(userModels);
        }

        public ActionResult Find(string name)
        {
            if (name is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = repo.Users.GetAll();
            var userModels = users.Select(x => MapDAL2Model(x));

            ViewBag.name = name;

            return View(userModels);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = repo.Users.Get((int)id);
            if (user == null || user.id == 0)
            {
                return HttpNotFound();
            }

            var userModel = MapDAL2Model(user);

            return View(userModel);
        }

        public ActionResult Create()
        {
            var roles = repo.Roles.GetAll();
            var roleModels = roles.Select(x => RoleController.MapDAL2Model(x));
            SelectList roleModelsSelectList = new SelectList(roleModels, "RoleId", "Name");
            ViewBag.Roles = roleModelsSelectList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,Username,UserId")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = MapModel2DAL(userModel);
                repo.Users.Save(user);
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = repo.Users.Get((int)id);
            if (user == null || user.id == 0)
            {
                return HttpNotFound();
            }

            var userModel = MapDAL2Model(user);

            var roles = repo.Roles.GetAll();
            var roleModels = roles.Select(x => RoleController.MapDAL2Model(x));
            SelectList roleModelsSelectList = new SelectList(roleModels, "RoleId", "Name");
            ViewBag.Roles = roleModelsSelectList;

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,RoleId")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = MapModel2DAL(userModel);
                repo.Users.Save(user);
                return RedirectToAction("Index");
            }
            return View(userModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = repo.Users.Get((int)id);
            if (user == null || user.id == 0)
            {
                return HttpNotFound();
            }

            var userModel = MapDAL2Model(user);

            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Users.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
