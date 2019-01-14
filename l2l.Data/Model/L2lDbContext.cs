using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace l2l.Data.Model
{
    public class L2lDbContext : DbContext
    {
        public L2lDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}