using AttendanceTrackingSystem.Configration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AttendanceTrackingSystem.Models
{
    public class AttendanceDBContext : DbContext
    {
        public AttendanceDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Intake> Intake { get; set; }
        public DbSet<ItiProgram> ItiProgram { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<Msg> Msgs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            
        }

    }
}
