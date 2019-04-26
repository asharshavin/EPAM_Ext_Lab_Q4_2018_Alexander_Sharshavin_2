using System;

namespace DAL2Messenger
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime dateOfCreation { get; set; }
        public int role { get; set; }
        public string password { get; set; }
        public User()
        {
        }
    }
}
