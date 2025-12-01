using System.ComponentModel.DataAnnotations.Schema;
using Notes_Api.Notes.Model;



namespace Notes_Api.NoteCategories.Model
{


    [Table("note_categories")]
    public class NoteCategory
    {
        [Column("note_id")]
        public int NoteId { get; set; }

        [Column("category")]
        public CategoryType Category { get; set; }

        public Note Note { get; set; }
    }
}