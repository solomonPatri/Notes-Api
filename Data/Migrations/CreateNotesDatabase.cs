using FluentMigrator;

namespace Notes_Api.Data.Migrations
{
    [Migration(20251199)]
    public class CreateNotesDatabase : Migration
    {
        private const string DatabaseName = "notes_db";

        public override void Up()
        {
            Execute.Sql($"CREATE DATABASE IF NOT EXISTS `{DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
            Execute.Sql($"USE `{DatabaseName}`;");
        }

        public override void Down()
        {
            Execute.Sql($"DROP DATABASE IF EXISTS `{DatabaseName}`;");
        }
    }
}
