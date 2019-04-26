using System;

namespace MVC2Messenger.Models
{
    public class MessageModel
    {
        public int MsgId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public UserModel User;
        public DateTime Date = DateTime.Now;
        public string Text = "";
    }
}