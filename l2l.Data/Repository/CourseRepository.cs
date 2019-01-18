using System;
using l2l.Data.Model;

namespace l2l.Data.Repository
{
    public class CourseRepository
    {
        private readonly L2lDbContext db;

        public CourseRepository()
        {
            var factory = new L2lDbContextFactory();
            db = factory.CreateDbContext(new string[] {});
        }

        public void Add(Course entity)
        {
            db.Courses.Add(entity);
        }

        public Course GetById(int Id)
        {
            return db.Courses.Find(Id);
        }
    }
}