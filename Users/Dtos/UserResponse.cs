using System.Collections.Generic;
using Notes_Api.Notes.Dtos;

namespace Notes_Api.Users.Dtos
{
    public class UserResponse
    {


        public int Id { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public ICollection<NoteResponse> Notes { get; set; } = new List<NoteResponse>();





    }
}
