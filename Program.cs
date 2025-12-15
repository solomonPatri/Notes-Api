using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Notes_Api.Data;
using Notes_Api.Notes.Repository;
using Notes_Api.Notes.Services;
using Notes_Api.Users.Repository;
using Notes_Api.Users.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Notes-Api", domain => domain.WithOrigins("")
                .AllowAnyHeader()
                .AllowAnyMethod());
        });

        var connectionString = builder.Configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Database connection string 'Default' is not configured.");

        EnsureDatabaseExists(connectionString);

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString,
                serverVersion,
                mySqlOptions =>
                {
                    mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
                }));

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<INoteRepo, NoteRepo>();
        builder.Services.AddScoped<INoteQueryService, NoteQueryService>();
        builder.Services.AddScoped<INoteCommandService, NoteCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
                Console.WriteLine("Migration successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");
            }
        }

        app.UseCors("Notes-Api");
        app.Run();
    }

    private static void EnsureDatabaseExists(string connectionString)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var database = builder.Database;

        if (string.IsNullOrWhiteSpace(database))
        {
            throw new InvalidOperationException("Database name must be specified in the connection string.");
        }

        builder.Database = string.Empty;

        var retries = 0;
        const int maxRetries = 5;

        while (true)
        {
            try
            {
                using var connection = new MySqlConnection(builder.ConnectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{database}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                command.ExecuteNonQuery();
                return;
            }
            catch (MySqlException)
            {
                retries++;
                if (retries >= maxRetries)
                {
                    throw;
                }

                Thread.Sleep(TimeSpan.FromSeconds(2 * retries));
            }
        }
    }




























}



