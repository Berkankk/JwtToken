using System.ComponentModel.DataAnnotations;

namespace JwtToken.Models
{
    public class CreateRoleDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
