using System.ComponentModel.DataAnnotations;

namespace MVC2Messenger.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}