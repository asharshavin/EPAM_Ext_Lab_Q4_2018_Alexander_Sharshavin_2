using DAL2Messenger;
using MVC2Messenger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC2Messenger.Controllers
{
    [Authorize]
    public class ChatUserController : Controller
    {
        private const string MessengerConnectionString = "MessengerConection";
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public static ChatUserModel MapDAL2Model(ChatUser objectDAL)
        {
            return new ChatUserModel
            {
                UserId = objectDAL.UserId,
                ChatId = objectDAL.ChatId,
            };
        }

        public static ChatUser MapModel2DAL(ChatUserModel objectModel)
        {
            return new ChatUser
            {
                UserId = objectModel.UserId,
                ChatId = objectModel.ChatId,
            };
        }

        public ActionResult Index(int? Chatid)
        {

            if (Chatid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chatUsers = repo.ChatUsers.GetChatUsers((int)Chatid);
            var chatUsersModels = chatUsers.Select(x => MapDAL2Model(x));

            return View(chatUsersModels);

        }

        public ActionResult Create(int id)
        {
            if (ModelState.IsValid)
            {
               var chatUser = new ChatUser()
                {
                    UserId = ChatController.messengerModel.CurrentUser.UserId,//pn прям раздолье для NullReferenceException
				   ChatId = id,
                };
                repo.ChatUsers.Save(chatUser);
                return RedirectToAction("Index", "Chat", new { id = id });
            }

            return RedirectToAction("Index", "Chat", new { id = id});
        }



    }
}