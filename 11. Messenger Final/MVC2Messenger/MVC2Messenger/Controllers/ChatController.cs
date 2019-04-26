using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC2Messenger.Models;
using DAL2Messenger;
using System.Web.Security;

namespace MVC2Messenger.Controllers
{
    public class ChatController : Controller
    {
        public static MessengerModel messengerModel;

        private const string MessengerConnectionString = "MessengerConection";
        private UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public static ChatModel MapDAL2Model(Chat objectDAL)
        {
            return new ChatModel
            {
                ChatId = objectDAL.ChatId,
                Name = objectDAL.Name,
            };
        }

        public static Chat MapModel2DAL(ChatModel objectModel)
        {
            return new Chat
            {
                ChatId = objectModel.ChatId,
                Name = objectModel.Name,
            };
        }

        [Authorize]
        public ActionResult Index(int? id)
        {
            if (messengerModel == null)
            {
                messengerModel = new MessengerModel();
            }

            var currentUser = UserController.MapDAL2Model(repo.Users.GetAll().FirstOrDefault(u => u.name == User.Identity.Name));

            var chats = repo.Chats.GetAllChatUser(currentUser.UserId);
            var chatModels = chats.Select(x => MapDAL2Model(x)).ToList();
            messengerModel.Chats = chatModels;

            if (id != null)
            {
                var chat = repo.Chats.Get((int)id);

                if (chat == null)
                {
                    return HttpNotFound();
                }

                messengerModel.CurrentChat = MapDAL2Model(chat);
            }

            if (messengerModel.CurrentChat == null && messengerModel.Chats.Count > 0)
            {
                messengerModel.CurrentChat = messengerModel.Chats[0];
            }

            if (messengerModel.CurrentChat != null )
            {
                messengerModel.CurrentChat.Messages = repo.Messages.GetChatMessages(messengerModel.CurrentChat.ChatId, 100).Select(x => MessageController.MapDAL2Model(x)).ToList();
            }

            messengerModel.CurrentUser = currentUser;

            var rolePrincipal = (RolePrincipal)User;
            var IsAdmin = rolePrincipal.IsInRole("Admin");

            ViewBag.IsAdmin = IsAdmin;

            return View(messengerModel);
        }

        public ActionResult Find(string name)
        {
            if (name is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.name = name;
            return View(messengerModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = repo.Chats.Get((int)id);
            if (chat == null || chat.ChatId == 0)
            {
                return HttpNotFound();
            }

            return View(MapDAL2Model(chat));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatId,Name")] ChatModel chatModel)
        {
            if (ModelState.IsValid)
            {
                var chat = MapModel2DAL(chatModel);
                repo.Chats.Save(chat);
                return RedirectToAction("Index");
            }

            return View(chatModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = repo.Chats.Get((int)id);
            if (chat == null || chat.ChatId == 0)
            {
                return HttpNotFound();
            }
            return View(MapDAL2Model(chat));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatId,Name")] ChatModel chatModel)
        {
            if (ModelState.IsValid)
            {
                var chat = MapModel2DAL(chatModel);
                repo.Chats.Save(chat);
                return RedirectToAction("Index");
            }
            return View(chatModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = repo.Chats.Get((int)id);
            if (chat == null || chat.ChatId == 0)
            {
                return HttpNotFound();
            }

            return View(MapDAL2Model(chat));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Chats.Delete(id);
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