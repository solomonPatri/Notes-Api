using System.ComponentModel.DataAnnotations;

namespace Notes_Api.Users.Dtos
{
    public class UserRequest
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }









    }
}
