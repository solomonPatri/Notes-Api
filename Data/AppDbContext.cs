using Microsoft.EntityFrameworkCore;
using Notes_Api.Notes.Model;
using Notes_Api.Users;
using Notes_Api.NoteCategories.Model;
using Notes_Api.Users.Model;

namespace Notes_Api.Data
{
    public class AppDbContext:DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {



        }


        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<User>()
               .HasMany(u => u.Notes)
               .WithOne(n => n.User)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade);


            modelbuilder.Entity<NoteCategory>()
                .HasKey(nc => new { nc.NoteId, nc.Category });



            modelbuilder.Entity<NoteCategory>()
                .HasOne(nc => nc.Note)
                .WithMany(n => n.NoteCategories)
                .HasForeignKey(nc => nc.NoteId)
                .OnDelete(DeleteBehavior.Cascade);










        } 











    }
}
