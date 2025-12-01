using Notes_Api.Notes.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notes_Api.Users.Dtos
{
    public class UserResponse
    {


        public int Id { get; set; }

        
        public string Email { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

       
        public ICollection<Note> Notes { get; set; } = new List<Note>();





    }
}
