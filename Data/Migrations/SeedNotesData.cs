

using FluentMigrator;

namespace Notes_Api.Data.Migrations
{
    [Migration(20251202)]
    public class SeedNotesData : Migration
    {
        public override void Down()
        {
             Execute.Sql(@"
                DELETE FROM note_categories 
                WHERE note_id IN (SELECT id FROM notes WHERE title IN ('Note A', 'Note B', 'Note C', 'Note D'));
                
                DELETE FROM notes WHERE title IN ('Note A', 'Note B', 'Note C', 'Note D');
                
                DELETE FROM users WHERE email LIKE 'user%@example.com';
            ");
           
        }

        public override void Up()
        {
            var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(),"Data", "Migrations", "scripts", "seed.sql");

            if (!File.Exists(sqlFilePath))
            {
                throw new FileNotFoundException($"SQL seed file not found at path: {sqlFilePath}");
            }

            string sqlContent = File.ReadAllText(sqlFilePath);
            Execute.Sql(sqlContent);
        }
    }
}
