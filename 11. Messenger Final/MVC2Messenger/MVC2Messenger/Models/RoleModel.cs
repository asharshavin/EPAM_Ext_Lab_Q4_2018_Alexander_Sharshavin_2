using System.ComponentModel.DataAnnotations;

namespace MVC2Messenger.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}