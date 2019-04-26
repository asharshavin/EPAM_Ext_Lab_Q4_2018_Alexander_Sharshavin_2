using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MVC2Messenger.Models;
using DAL2Messenger;
using System.Web.Security;

namespace MVC2Messenger.Controllers
{
    public class MessageController : Controller
    {
        private const string MessengerConnectionString = "MessengerConection";
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public static MessageModel MapDAL2Model(Message objectDAL)
        {
            return new MessageModel
            {
                MsgId = objectDAL.MsgId,
                Text = objectDAL.Text,
                UserId = objectDAL.UserId,
                User = UserController.MapDAL2Model(repo.Users.Get(objectDAL.UserId)),
                ChatId = objectDAL.ChatId,
                Date = objectDAL.Date
            };
        }

        public static Message MapModel2DAL(MessageModel objectModel)
        {
            return new Message
            {
                MsgId = objectModel.MsgId,
                Text = objectModel.Text,
                UserId = objectModel.UserId,
                ChatId = objectModel.ChatId,
                Date = objectModel.Date
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Text)
        {
            if (ModelState.IsValid)
            {
                var message = new Message()
                {
                    Text = Text,
                    UserId = ChatController.messengerModel.CurrentUser.UserId,
                    ChatId = ChatController.messengerModel.CurrentChat.ChatId,
                };
                repo.Messages.Save(message);
                return RedirectToAction("Index", "Chat");
            }

            return RedirectToAction("Index", "Chat");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = repo.Messages.Get((int)id);
            if (message == null || message.MsgId == 0)
            {
                return HttpNotFound();
            }

            var rolePrincipal = (RolePrincipal)User;
            if (!rolePrincipal.IsInRole("Admin") && (DateTime.Now - message.Date).TotalSeconds > ChatModel.MessageTimeEditInSeconds)
            {
                return RedirectToAction("Index", "Chat");
            }

            return View(MapDAL2Model(message));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgId, Text")] MessageModel messageModel)

        {
            if (ModelState.IsValid)
            {
                var message = new Message()
                {
                    MsgId = messageModel.MsgId, 
                    Text = messageModel.Text,
                    UserId = ChatController.messengerModel.CurrentUser.UserId,
                    ChatId = ChatController.messengerModel.CurrentChat.ChatId,
                };
                repo.Messages.Save(message);
                return RedirectToAction("Index", "Chat");
            }
            return View(messageModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = repo.Messages.Get((int)id);
            if (message == null || message.MsgId == 0)
            {
                return HttpNotFound();
            }

            var rolePrincipal = (RolePrincipal)User;
            if (!rolePrincipal.IsInRole("Admin") && (DateTime.Now - message.Date).TotalSeconds > ChatModel.MessageTimeEditInSeconds)
            {
                return RedirectToAction("Index", "Chat");
            }

            return View(MapDAL2Model(message));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Messages.Delete(id);

            return RedirectToAction("Index", "Chat");
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