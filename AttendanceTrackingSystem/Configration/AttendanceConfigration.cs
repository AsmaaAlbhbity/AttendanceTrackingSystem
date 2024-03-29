using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceTrackingSystem.Configration
{
    public class AttendanceConfigration : IEntityTypeConfiguration<Attendance>
    {

        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.UseTptMappingStrategy();
            builder.HasIndex(a => new { a.UserId, a.Date }).IsUnique();

        }
    }
}
