using System.ComponentModel.DataAnnotations;

namespace Notes_Api.Notes.Dtos
{
    public class NoteRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }






    }
}
