using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceTrackingSystem.Configration
{
    public class StudentConfigration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //        builder.Property(e => e.GraduationYear)
            //            .HasAnnotation("SqlServer:CheckConstraint", "[GraduationYear] >= DATEADD(year, -5, GETDATE())");
             builder.HasOne(a=>a.Track).WithMany(a=>a.Students).HasForeignKey(b=>b.TrackId).OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
