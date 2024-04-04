using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Schedule> GetFutureStudentSchedule(int studentId)
        {
            var today = DateTime.Today;
          
            var futureSchedules = db.Schedules
                .Where(s => s.Track.Students.Any(st => st.UserId == studentId) && s.Date >= today)
                .ToList();

            return futureSchedules;
        }

        public string GetTrackNameByUserId(int userId)
        {
            var student = db.Students.FirstOrDefault(s => s.UserId == userId);
            if (student != null)
            {
                var track = db.Tracks.FirstOrDefault(t => t.TrackId == student.TrackId);
                if (track != null)
                {
                    return track.Name;
                }
            }
            return null;
        }

        public string GetSupervisorNameByUserId(int userId)
        {
            var student = db.Students.FirstOrDefault(s => s.UserId == userId);
            if (student != null)
            {
                var track = db.Tracks.FirstOrDefault(t => t.TrackId == student.TrackId);
                if (track != null)
                {
                    return db.Instructors.FirstOrDefault(i=>i.UserId==track.SupervisorId).Name;
                }
            }
            return null;
        }
    }
}

