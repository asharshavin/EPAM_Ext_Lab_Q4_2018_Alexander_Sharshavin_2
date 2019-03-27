using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2Messenger.Models
{
    public class Chat
    {
        public int ChatId { get; set; }

        public string Name { get; set; }

        public List<User> Users;

        public List<ChatMessage> Messages;

        public Chat()
        {
            Users = new List<User>();
            Messages = new List<ChatMessage>();
        }
    }
}