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
            return db.Students.ToList();

        }

        public Student getById(int id)
        {
            return db.Students.FirstOrDefault(a => a.UserId == id);
        }

        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Students.FirstOrDefault(a => a.UserId == id);
            db.Students.Remove(obj);
            db.SaveChanges();
        }
    }
}
