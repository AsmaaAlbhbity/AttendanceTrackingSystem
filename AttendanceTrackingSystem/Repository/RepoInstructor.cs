using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoInstructor : IRepoInstructor
    {
        AttendanceDBContext db;
        public RepoInstructor(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Instructors.FirstOrDefault(a => a.UserId == id);
            db.Instructors.Remove(obj);
            db.SaveChanges();
        }

        public List<Instructor> getAll()
        {
            return db.Instructors.ToList();
        }

        public Instructor getById(int id)
        {
            return db.Instructors.FirstOrDefault(a => a.UserId == id);
        }

        public void Update(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }

  
    }
}
