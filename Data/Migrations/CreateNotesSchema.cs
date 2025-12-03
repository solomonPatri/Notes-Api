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
                .WithColumn("user_id").AsInt32().NotNullable()
                    .ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("title").AsString(255).NotNullable()
                .WithColumn("content").AsString(int.MaxValue).Nullable() // TEXT / LONGTEXT
                .WithColumn("is_archived").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().Nullable();

            // NOTE CATEGORIES (enum CategoryType stocat ca int)
            Create.Table("note_categories")
                .WithColumn("note_id").AsInt32().NotNullable()
                    .ForeignKey("notes", "id").OnDelete(Rule.Cascade)
                .WithColumn("category").AsInt32().NotNullable();

            // Cheie primară compusă pentru tabelul de legătură
            Create.PrimaryKey("PK_note_categories")
                .OnTable("note_categories")
                .Columns("note_id", "category");
        }

        public override void Down()
        {
            Delete.Table("note_categories");
            Delete.Table("notes");
            Delete.Table("users");
        }
    }
}
