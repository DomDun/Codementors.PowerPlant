using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Member
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
