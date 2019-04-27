using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC2Messenger.Models;
using DAL2Messenger;

namespace MVC2Messenger.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private const string MessengerConnectionString = "MessengerConection";
        private UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public static RoleModel MapDAL2Model(Role objectDAL)
        {
            return new RoleModel
            {
                RoleId = objectDAL.id,
                Name = objectDAL.name,
            };
        }

        [Authorize]
        public ActionResult Index()
        {
            var roles = repo.Roles.GetAll();
            var roleModels = roles.Select(x => MapDAL2Model(x));

            return View(roleModels);
        }

        [Authorize]
        public ActionResult Find(string name)
        {
            if (name is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roles = repo.Roles.GetAll();
            var roleModels = roles.Select(x => MapDAL2Model(x));
            ViewBag.name = name;
            return View(roleModels);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = repo.Roles.Get((int)id);
            if (role == null || role.id == 0)//pn хардкод
			{
                return HttpNotFound();
            }

            var roleModel = MapDAL2Model(role);

            return View(roleModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Role role)
        {
            if (ModelState.IsValid)
            {
                repo.Roles.Save(role);
                return RedirectToAction("Index");
            }

            var roleModel = MapDAL2Model(role);

            return View(roleModel);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = repo.Roles.Get((int)id);
            if (role == null || role.id == 0)
            {
                return HttpNotFound();
            }

            var roleModel = MapDAL2Model(role);

            return View(roleModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Role role)
        {
            if (ModelState.IsValid)
            {
                repo.Roles.Save(role);
                return RedirectToAction("Index");
            }

            var roleModel = MapDAL2Model(role);

            return View(roleModel);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = repo.Roles.Get((int)id);
            if (role == null || role.id == 0)
            {
                return HttpNotFound();
            }

            var roleModel = MapDAL2Model(role);

            return View(roleModel);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Roles.Delete(id);
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
