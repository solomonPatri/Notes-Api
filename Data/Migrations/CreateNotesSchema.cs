using FluentMigrator;
using System.Data;

namespace NotesApi.Data.Migrations
{
    [Migration(20251201)]
    public class CreateNotesSchema : Migration
    {
        public override void Up()
        {
            // USERS
            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("email").AsString(255).NotNullable()
                .WithColumn("password").AsString(255).NotNullable()
                .WithColumn("age").AsInt32().NotNullable();

            // NOTES
            Create.Table("notes")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("userId").AsInt32().NotNullable()
                    .ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("title").AsString(255).NotNullable()
                .WithColumn("content").AsString(int.MaxValue).Nullable() // TEXT / LONGTEXT
                .WithColumn("isArchived").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("createdAt").AsDateTime().NotNullable()
                .WithColumn("updatedAt").AsDateTime().Nullable();

            // NOTE CATEGORIES (enum CategoryType stocat ca int)
            Create.Table("noteCategories")
                .WithColumn("noteId").AsInt32().NotNullable()
                    .ForeignKey("notes", "id").OnDelete(Rule.Cascade)
                .WithColumn("category").AsInt32().NotNullable();

            // Cheie primară compusă pentru tabelul de legătură
            Create.PrimaryKey("PK_noteCategories")
                .OnTable("noteCategories")
                .Columns("noteId", "category");
        }

        public override void Down()
        {
            Delete.Table("noteCategories");
            Delete.Table("notes");
            Delete.Table("users");
        }
    }
}