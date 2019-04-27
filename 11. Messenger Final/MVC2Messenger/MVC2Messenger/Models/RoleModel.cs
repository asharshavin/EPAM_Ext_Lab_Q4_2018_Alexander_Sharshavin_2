using System.ComponentModel.DataAnnotations;

namespace MVC2Messenger.Models
{
    public class RoleModel//pn мне кажется, что кое-кто забыл запускать stylecop
	{
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}