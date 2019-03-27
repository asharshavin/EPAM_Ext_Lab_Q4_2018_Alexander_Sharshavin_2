using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC2Messenger.Models;

namespace MVC2Messenger.Controllers
{
    public class MessageController : Controller
    {

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var msg = ChatController.chatModel.CurrentChat.Messages[(int)id];
            if (msg == null)
            {
                return HttpNotFound();
            }
            return View(msg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgId,Name")] ChatMessage msg)
        {
            if (ModelState.IsValid)
            {
                ChatController.chatModel.CurrentChat.Messages.Insert(msg.MsgId, msg);

                return RedirectToAction("Index", "Chat");
            }
            return View(msg);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var msg = ChatController.chatModel.CurrentChat.Messages[(int)id];
            if (msg == null)
            {
                return HttpNotFound();
            }
            return View(msg);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var msg = ChatController.chatModel.CurrentChat.Messages[id];
            ChatController.chatModel.CurrentChat.Messages.Remove(msg);

            return RedirectToAction("Index", "Chat");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgId,Name")] ChatMessage msg)
        {
            if (ModelState.IsValid)
            {
                msg.MsgId = ChatController.chatModel.CurrentChat.Messages.Count;
                ChatController.chatModel.CurrentChat.Messages.Add(msg);
                return RedirectToAction("Index", "Chat");
            }

            return View(msg);
        }
    }
}