using System.Collections.Generic;

namespace MVC2Messenger.Models
{
    public class MessengerModel
    {
        public ChatModel CurrentChat;
        public UserModel CurrentUser;

        public List<ChatModel> Chats;

        public MessengerModel()
        {
            Chats = new List<ChatModel>();
        }
    }
}