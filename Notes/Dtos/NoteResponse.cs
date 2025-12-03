using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notes_Api.NoteCategories.Model;

namespace Notes_Api.Notes.Dtos
{
    public class NoteResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public bool IsArchived { get; set; } = false;

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public List<CategoryType> Categories { get; set; } = new();




    }
}
