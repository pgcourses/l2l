using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace l2l.Data.Model
{
    public class L2lDbContextFactory : IDesignTimeDbContextFactory<L2lDbContext>
    {
        public L2lDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<L2lDbContext>();

            builder.UseSqlite("Data Source=l2l.db;");

            return new L2lDbContext(builder.Options);
        }
    }
}