using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repository
{
    
    public class RepoAttendance : IRepoAttendance
    {
        AttendanceDBContext db;
        public RepoAttendance(AttendanceDBContext _db)
        {
            db= _db;
        }

        public void Add(Attendance attendance)
        {
            db.Attendances.Add(attendance);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Attendances.FirstOrDefault(a => a.AttendanceId == id);
            db.Attendances.Remove(obj);
            db.SaveChanges();
        }

        public List<Attendance> getAll()
        {
            return db.Attendances.ToList();
        }

        public Attendance getById(int id)
        {
            return db.Attendances.FirstOrDefault(a=>a.AttendanceId==id);
        }

        public void Update(Attendance attendance)
        {
            db.Attendances.Update(attendance);
            db.SaveChanges();
        }


        ////////////asmaa 

        public List<Attendance> GetUserAttendance(int userId, DateTime startDate, DateTime endDate)
        {
      
            return db.Attendances
                .Where(a => a.UserId == userId && a.Date >= startDate && a.Date <= endDate )
                .ToList();


        }
       


        //admin charts

        public List<AttendanceCountPerUserType> GetAttendanceCountsPerUserType()
        {
            var attendanceCountsPerUserType = db.Attendances
                .GroupJoin(db.Users,
                           a => a.UserId,
                           u => u.UserId,
                           (a, u) => new { Attendance = a, User = u.FirstOrDefault() })
                .Where(x => x.User != null) // Exclude entries with no corresponding user
                .GroupBy(x => x.User.UserType)
                .Select(g => new AttendanceCountPerUserType
                {
                    UserType = g.Key,
                    AttendanceCount = g.Count(),
                    AttendancePercentage = (double)g.Count() / db.Attendances.Count()
                })
                .ToList();

            return attendanceCountsPerUserType;
        }

        // calendar


        public List<AttendanceRecordViewModel> GetLateOrAbsentDates(int userId)
        {
            var lateOrAbsentDates = db.Attendances
                .Where(a => a.UserId == userId && (a.Status == AttendaneStatus.Late || a.Status == AttendaneStatus.Absent))
                .Select(a => new AttendanceRecordViewModel
                {
                    Date = a.Date,
                    Status = a.Status
                })
                .ToList();

            return lateOrAbsentDates;
        }

    }
}
