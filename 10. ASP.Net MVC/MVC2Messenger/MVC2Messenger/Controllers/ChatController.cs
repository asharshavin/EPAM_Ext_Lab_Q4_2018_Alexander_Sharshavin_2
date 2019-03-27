using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC2Messenger.Models;

namespace MVC2Messenger.Controllers
{
    public class ChatController : Controller
    {
        public static ChatModel chatModel;

        private UserContext db = new UserContext();

        public ActionResult Index(int? id)
        {
            if (chatModel == null) chatModel = new ChatModel();

            if (id == null)
            {
                id = 0;
            }
            var chat = chatModel.Chats[(int)id];
            if (chat == null)
            {
                return HttpNotFound();
            }

            chatModel.CurrentChat = chat;

            return View(chatModel);
        }
        public ActionResult Find(string name)
        {
            if (name is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.name = name;
            return View(chatModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = chatModel.Chats[(int) id];
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatId,Name")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                chat.ChatId = chatModel.Chats.Count;
                chatModel.Chats.Add(chat);
                return RedirectToAction("Index");
            }

            return View(chat);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = chatModel.Chats[(int)id];
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatId,Name")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                chatModel.Chats.Insert(chat.ChatId, chat);

                return RedirectToAction("Index");
            }
            return View(chat);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = chatModel.Chats[(int) id];
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var chat = chatModel.Chats[id];
            chatModel.Chats.Remove(chat);

            return RedirectToAction("Index");
        }

    }
}