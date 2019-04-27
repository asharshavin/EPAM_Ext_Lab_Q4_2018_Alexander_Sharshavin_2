using System;

namespace MVC2Messenger.Models
{
    public class MessageModel//pn а здесь что с отступами вдруг случилось?
	{
        public int MsgId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public UserModel User;//pn почему не свойством?
		public DateTime Date = DateTime.Now;//pn почему не свойством? Значение лучше присваивать в момент (лучше после) отправки сообщения. И это лучше делать на клиенте (получать из браузера).
		public string Text = "";//pn почему не свойством?
	}
}