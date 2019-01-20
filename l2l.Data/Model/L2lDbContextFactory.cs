using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace l2l.Data.Model
{
    public class L2lDbContextFactory : IDesignTimeDbContextFactory<L2lDbContext>, IDisposable
    {
        private readonly string cn;
        private readonly SqliteConnection connection = null;

        public bool IsInMemoryDb()
        {
            var cb = new SqlConnectionStringBuilder(cn);
            if (!cb.ContainsKey(GlobalStrings.DataSource))
            {
                throw new ArgumentException("missing property from ConnectionString: Data Source", "ConnectionString");
            }
            return GlobalStrings.SqlMemoryDb.Equals((string)cb[GlobalStrings.DataSource], 
                                StringComparison.OrdinalIgnoreCase);
        }

        public L2lDbContextFactory()
        {
            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable(GlobalStrings.AspnetCoreEnvironment);

            var cbuilder = new ConfigurationBuilder()
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environment}.json", true)
                                .AddEnvironmentVariables()
                                ;

            var config = cbuilder.Build();
            cn = config.GetConnectionString(GlobalStrings.ConnectionName);
            if (IsInMemoryDb())
            {
                connection = new SqliteConnection(cn);
                connection.Open();
            }
        }

        public L2lDbContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<L2lDbContext>();

            if (IsInMemoryDb())
            {
                obuilder.UseSqlite(connection);
            }
            else
            {
                obuilder.UseSqlite(cn);
            }

            return new L2lDbContext(obuilder.Options);
        }
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}