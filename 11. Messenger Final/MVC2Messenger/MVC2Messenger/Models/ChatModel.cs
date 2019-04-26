using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MVC2Messenger.Models
{
    public class ChatModel
    {
        public static double MessageTimeEditInSeconds = 60 ;

        public int ChatId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<UserModel> Users;

        public List<MessageModel> Messages;

        public ChatModel()
        {
            Users = new List<UserModel>();
            Messages = new List<MessageModel>();
        }
    }
}