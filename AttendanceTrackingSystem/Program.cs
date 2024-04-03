using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AttendanceDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

            builder.Services.AddScoped<IRepoAttendance,RepoAttendance>();
            builder.Services.AddScoped<IRepoEmployee, RepoEmployee>();
            builder.Services.AddScoped<IRepoInstructor, RepoInstructor>();
            builder.Services.AddScoped<IRepoIntake, RepoIntake>();
            builder.Services.AddScoped<IRepoItiProgram, RepoItiProgram>();
            builder.Services.AddScoped<IRepoPermission, RepoPermission>();
            builder.Services.AddScoped<IRepoSchedule, RepoSchedule>();
            builder.Services.AddScoped<IRepoStudent, RepoStudent>();
            builder.Services.AddScoped<IRepoStudentAttendance, RepoStudentAttendance>();
            builder.Services.AddScoped<IRepoTrack, RepoTrack>();
            builder.Services.AddScoped<IRepoUser, RepoUser>();

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
