using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebDiaryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<DiaryEntry> DiaryEntries { get;set;}
    }
}