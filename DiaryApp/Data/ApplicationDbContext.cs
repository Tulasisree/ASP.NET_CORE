using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp
{
    public class ApplicationDbContext : DbContext
    {
        // passes options to base class DBContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<DiaryEntry> DiaryEntries { get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry 
                {
                    Id = 1, 
                    Title="Went Hiking", 
                    Content="Went hiking with friends",
                    Created = new DateTime(2024, 1, 1)
                },
                new DiaryEntry 
                {
                    Id = 2, 
                    Title="Went shopping", 
                    Content="Went shopping with friends",
                    Created = new DateTime(2024, 1, 1)
                },
                new DiaryEntry 
                {
                    Id = 3, 
                    Title="Went diving", 
                    Content="Went diving with friends",
                    Created = new DateTime(2024, 1, 1)
                }
            );
        }
    }
}