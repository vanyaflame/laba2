using Microsoft.EntityFrameworkCore;
using SimpleTaskApp.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace SimpleTaskApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}