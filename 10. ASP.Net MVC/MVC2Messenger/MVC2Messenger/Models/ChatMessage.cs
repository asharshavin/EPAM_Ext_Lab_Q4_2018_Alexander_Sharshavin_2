using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2Messenger.Models
{
    public class ChatMessage
    {
        public int MsgId { get; set; }
        public User User;
        public DateTime Date = DateTime.Now;
        public string Text = "";
    }
}