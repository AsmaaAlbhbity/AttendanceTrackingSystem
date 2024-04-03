using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoStudent : IRepoStudent
    {
        AttendanceDBContext db;
        public RepoStudent(AttendanceDBContext _db)
        {
            db = _db;
        }
        public List<Student> getAll()
        {
            return db.Users.OfType<Student>().ToList();

        }

        public Student getById(int id)
        {
            return db.Users.OfType<Student>().FirstOrDefault(a => a.UserId == id);
        }

        public void Add(Student student)
        {
            db.Users.Add(student);
            db.SaveChanges();
        }

        public void Update(Student student)
        {
            db.Users.Update(student);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Users.FirstOrDefault(a => a.UserId == id);
            if (obj != null)
            {
                db.Users.Remove(obj);
                db.SaveChanges();
            }
        }
        public List<Student> GetPaginatedStudents(int page, int pageSize)
        {
            return db.Users.OfType<Student>().Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
