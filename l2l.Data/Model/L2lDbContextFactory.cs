using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace l2l.Data.Model
{
    public class L2lDbContextFactory : IDesignTimeDbContextFactory<L2lDbContext>
    {
        public L2lDbContext CreateDbContext(string[] args)
        {
            var obuilder = new DbContextOptionsBuilder<L2lDbContext>();

            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable(GlobalStrings.AspnetCoreEnvironment);

            var cbuilder = new ConfigurationBuilder()
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environment}.json", true)
                                .AddEnvironmentVariables()
                                ;

            var config = cbuilder.Build();

            var cn = config.GetConnectionString(GlobalStrings.ConnectionName);

            obuilder.UseSqlite(cn);

            return new L2lDbContext(obuilder.Options);
        }
    }
}