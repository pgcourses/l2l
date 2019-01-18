using System;
using System.Linq;
using l2l.Data.Model;

namespace l2l.Data.Repository
{
    public class CourseRepository
    {
        private readonly L2lDbContext db;

        public CourseRepository()
        {
            //TODO: Antipattern
            var factory = new L2lDbContextFactory();
            db = factory.CreateDbContext(new string[] {});
        }

        public void Add(Course course)
        {
            //TODO: Async
            db.Courses.Add(course);
        }

        public Course GetById(int Id)
        {
            //TODO: Async
            return db.Courses.Find(Id);
        }

        public void Update(Course course)
        {
            //TODO: return with void?
            db.Courses.Update(course);
        }

        public void Remove(Course course)
        {
            //TODO: return with void?
            db.Courses.Remove(course);
        }
    }
}