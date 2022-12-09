using System.ComponentModel.DataAnnotations;

namespace University_Backend.Models.DataModels
{
    public class UserLoginDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}