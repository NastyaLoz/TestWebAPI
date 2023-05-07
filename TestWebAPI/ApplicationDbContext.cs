using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Models;

namespace TestWebAPI
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Words> Words { get; set; }
        public DbSet<Types> Types { get; set; }
        
        public DbSet<CompletedTasks> CompletedTasks { get; set; }
        public DbSet<WordsTasks> WordsTasks { get; set; }
        public DbSet<LearnedWords> LearnedWords { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompletedTasks>().HasKey(q => new {q.UserId, q.TaskId});
            modelBuilder.Entity<WordsTasks>().HasKey(q => new {q.TaskId, q.WordId});
            modelBuilder.Entity<LearnedWords>().HasKey(q => new {q.UserId, q.WordId});
        }
    }
}