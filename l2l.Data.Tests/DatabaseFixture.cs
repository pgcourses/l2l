using System;
using l2l.Data.Model;

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
            db.Database.EnsureCreated();
        }

        public L2lDbContext GetNewL2lDbContext()
        {
            return factory.CreateDbContext(new string[] {});
        }

        public void Dispose()
        {
            var db=GetNewL2lDbContext();
            db.Database.EnsureDeleted();
            db.Dispose();
        }
    }
}