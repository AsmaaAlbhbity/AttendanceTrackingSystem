using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;


namespace AttendanceTrackingSystem.Configration
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(a => a.Name).IsRequired();

            builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100) 
            .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            builder.HasIndex(a => a.Email).IsUnique();

            builder.Property(a => a.Phone)
            .HasMaxLength(11) 
            .HasColumnType("varchar(11)") 
            .HasAnnotation("RegularExpression", @"^(010|011|015)-\d{8}$");


            builder.HasDiscriminator<string>("UserType")
             .HasValue<User>("User")
             .HasValue<Student>("Student")
             .HasValue<Employee>("Employee")
             .HasValue<Instructor>("Instructor");


        }

    }




}
   