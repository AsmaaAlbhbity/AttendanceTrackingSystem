using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceTrackingSystem.Configration
{
    public class ItiProgramConfigration : IEntityTypeConfiguration<ItiProgram>
    {
        public void Configure(EntityTypeBuilder<ItiProgram> builder)
        {
            builder.HasData(
            new ItiProgram
            {
                ItiProgramId = 1,
                Name = ProgramName.PTP,
                Duration=9
            },
            new ItiProgram
            {
                ItiProgramId = 2,
                Name = ProgramName.ITP,
                Duration = 4
            },
            new ItiProgram
            {
                ItiProgramId = 3,
                Name = ProgramName.STP,
                Duration = 2
            }
        );

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
