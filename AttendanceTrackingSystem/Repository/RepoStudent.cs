using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            // check if email is already in use
            if (db.Users.Any(u => u.Email == student.Email.ToLower().Trim()))
            {
                throw new ValidationException("Email is already in use!");
            }
            // check if track is full   
            else if (IsTrackFull(student.TrackId))
            {
                throw new ValidationException("Track is Full!");
            }

            student.Email = student.Email.ToLower().Trim();
            student.StudentDegree = 250;
            db.Users.Add(student);
            db.SaveChanges();
        }
        private bool IsTrackFull(int trackId)
        {
            var track = db.Tracks.FirstOrDefault(t => t.TrackId == trackId);
            return track.Students.Count >= track.Capacity;
        }

        public void Update(Student student)
        {
            // check if email is already in use
            //if (db.Users.Any(u => u.Email == student.Email.ToLower().Trim()))
            //{
            //    throw new ValidationException("Email is already in use!");
            //}
            if (db.Users.Except(new List<Student>() { student }).Any(u => u.Email == student.Email.ToLower().Trim()))
            {
                throw new ValidationException("Email is already in use!");
            }
            student.Email = student.Email.ToLower().Trim();
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
        public bool IsImageExistedBefore(string imageName)
        {
            imageName = imageName.Split('_').First();
            return db.Users.Any(u => u.ImgUrl.StartsWith(imageName + "_"));
        }
        public List<Student> GetPaginatedStudents(int page, int pageSize)
        {
            return db.Users.OfType<Student>().Skip((page - 1) * pageSize).Take(pageSize).ToList();
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

        public Instructor GetSupervisorByStudentId(int userId)
        {
            var student = db.Students.FirstOrDefault(s => s.UserId == userId);
            if (student != null)
            {
                var track = db.Tracks.FirstOrDefault(t => t.TrackId == student.TrackId);
                if (track != null)
                {
                    return db.Instructors.FirstOrDefault(i=>i.UserId==track.SupervisorId);
                }
            }
            return null;
        }
        //asmaa
        public List<Student> GetPendingStudents()
        {
            return db.Students.Where(std=>std.IsApproved==Approve.pending).ToList();
        }
        public void ApproveStudent(int studentId)
        {
            db.Students.FirstOrDefault(std=>std.UserId==studentId).IsApproved=Approve.Accepted;
        }
        public void RejectStudent(int studentId)
        {
            db.Students.FirstOrDefault(std => std.UserId == studentId).IsApproved = Approve.Rejected;
        }
        public List<Attendance> GetStudentAttendance(DateTime selectedDate, int selectedTrackId)
        {
            var attendanceRecords = db.Attendances
                .Where(a => a.User is Student && ((Student)a.User).TrackId == selectedTrackId && a.Date.Date == selectedDate.Date)
                .ToList();

            return attendanceRecords;
        }
       








    }
}

