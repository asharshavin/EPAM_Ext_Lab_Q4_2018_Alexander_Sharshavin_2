using System;

namespace DAL2Messenger
{
    public class Message
    {
        public int MsgId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime Date = DateTime.Now;
        public string Text { get; set; } = "" ;
    }
}