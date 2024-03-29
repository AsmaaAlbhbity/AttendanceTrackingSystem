using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceTrackingSystem.Configration
{
    public class StudentAttendanceConfigration : IEntityTypeConfiguration<StudentAttendance>
    {
        public void Configure(EntityTypeBuilder<StudentAttendance> builder)
        {
            builder.HasOne(a=>a.StudentSchdule)
                .WithMany(b=>b.StudentAttendances).HasForeignKey(a=>a.SchduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
