using Microsoft.EntityFrameworkCore;
using University_Backend.Models.DataModels;

namespace University_Backend.Data
{
    public class DbContext_University : DbContext
    {
        public DbContext_University(DbContextOptions<DbContext_University> dbContextOptions) : base(dbContextOptions) { }

        // TODO: Add DBSets (tables of our Database)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
    }
}