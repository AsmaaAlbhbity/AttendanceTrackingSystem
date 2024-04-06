using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoStudentAttendance : IRepoStudentAttendance
    {
        AttendanceDBContext db;
        public RepoStudentAttendance(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(StudentAttendance studentAttendance)
        {
            db.StudentAttendances.Add(studentAttendance);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.StudentAttendances.FirstOrDefault(a => a.AttendanceId == id);
            db.StudentAttendances.Remove(obj);
            db.SaveChanges();
        }

        public List<StudentAttendance> getAll()
        {
            return db.StudentAttendances.ToList();
        }

        public StudentAttendance getById(int id)
        {
            return db.StudentAttendances.FirstOrDefault(a => a.AttendanceId == id);
        }

        public void Update(StudentAttendance studentAttendance)
        {
            db.StudentAttendances.Update(studentAttendance);
            db.SaveChanges();
        }

        //for only students

        public List<StudentAttendance> GetStudentAttendance(int studentId, DateTime startDate, DateTime endDate)
        {
            var student = db.Students.Find(studentId);
            if (student != null)
            {
              
                var schedules = db.Schedules
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .ToList();

            
                var studentAttendances = new List<StudentAttendance>();
                foreach (var schedule in schedules)
                {
                    var attendance = new StudentAttendance
                    {
                        Date = schedule.Date,
                        CheckIn = new TimeOnly(0, 0),
                        CheckOut = new TimeOnly(0, 0),
                        UserId = studentId,
                        StudentSchdule = schedule
                    };

                    studentAttendances.Add(attendance);
                }

                return studentAttendances;
            }
            return new List<StudentAttendance>(); 
        }

        public List<DateTime> GetStudentScheduleDates(int studentId, DateTime startDate, DateTime endDate)
        {
            var studentSchedules = db.Schedules
                .Where(s => s.StudentAttendances.Any(sa => sa.UserId == studentId) && s.Date >= startDate && s.Date <= endDate)
                .Select(s => s.Date)
                .ToList();

            return studentSchedules;
        }


    }
}
