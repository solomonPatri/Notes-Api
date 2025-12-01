using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Notes_Api.NoteCategories.Model;
using Notes_Api.Users.Model;

namespace Notes_Api.Notes.Model
{
    [Table("notes")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Required]
        [Column("is_archived")]
        public bool IsArchived { get; set; } = false;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; }

        public ICollection<NoteCategory> NoteCategories { get; set; } = new List<NoteCategory>();
    }
}