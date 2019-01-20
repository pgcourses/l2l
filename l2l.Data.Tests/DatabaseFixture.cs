using System;
using l2l.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace l2l.Data.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly L2lDbContextFactory factory;

        public DatabaseFixture()
        {
            //AP
            factory = new L2lDbContextFactory();
            var db=GetNewL2lDbContext();

            if (factory.IsInMemoryDb())
            {
                //in memory db
                db.Database.EnsureCreated();
            }
            else
            {
                //working only in file db!!!!
                //https://github.com/aspnet/EntityFrameworkCore/issues/9842
                db.Database.Migrate();
            }
        }

        public L2lDbContext GetNewL2lDbContext()
        {
            return factory.CreateDbContext(new string[] {});
        }

        public void Dispose()
        {
            var db=GetNewL2lDbContext();
            factory.Dispose();
            db.Database.EnsureDeleted();
            db.Dispose();
        }
    }
}