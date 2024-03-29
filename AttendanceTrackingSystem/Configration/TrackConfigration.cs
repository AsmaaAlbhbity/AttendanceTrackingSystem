using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AttendanceTrackingSystem.Configration
{
    public class TrackConfigration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder
             .HasMany(t => t.Instructors)
             .WithMany(i => i.Tracks)
             .UsingEntity<Dictionary<string, object>>(
                 "InstructorTrack",  // Define the name of the join table
                 j => j
                     .HasOne<Instructor>()  // Define the navigation property type for Instructor
                     .WithMany()
                     .HasForeignKey("InstructorId")  // Define the foreign key for Instructor
                     .OnDelete(DeleteBehavior.Restrict), // Specify the delete behavior for Instructor
                 j => j
                     .HasOne<Track>()  // Define the navigation property type for Track
                     .WithMany()
                     .HasForeignKey("TrackId")  // Define the foreign key for Track
                     .OnDelete(DeleteBehavior.Restrict) // Specify the delete behavior for Track
             );
        }
    }
}
