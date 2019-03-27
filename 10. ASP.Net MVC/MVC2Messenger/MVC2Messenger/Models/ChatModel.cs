using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2Messenger.Models
{
    public class ChatModel
    {
        public Chat CurrentChat;

        public List<Chat> Chats;

        public ChatModel()
        {
            Chats = new List<Chat>();
            Chats.Add(new Chat { ChatId = 0, Name = "vegamega" });
            Chats.Add(new Chat { ChatId = 1, Name = "vegamega2" });
            Chats[0].Messages.Add(new ChatMessage { MsgId = 0, Date = DateTime.Now, Text = "Привет всем в этом чате!" });
            Chats[1].Messages.Add(new ChatMessage { MsgId = 0, Date = DateTime.Now, Text = "Кто здесь?" });

            CurrentChat = Chats[0];
        }
    }
}