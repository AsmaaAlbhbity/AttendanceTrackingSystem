using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoInstructor : IRepoInstructor
    {
        AttendanceDBContext db;
        public RepoInstructor(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Instructors.FirstOrDefault(a => a.UserId == id);
            db.Instructors.Remove(obj);
            db.SaveChanges();
        }

        public List<Instructor> getAll()
        {
            return db.Instructors.ToList();
        }

        public Instructor getById(int id)
        {
            return db.Instructors.FirstOrDefault(a => a.UserId == id);
        }

        public void Update(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public void AddTrack(int TrackId, int InstructorId)
        {
            var track = db.Tracks.FirstOrDefault(a => a.TrackId == TrackId);
            var instructor = db.Instructors.FirstOrDefault(b => b.UserId == InstructorId);

            track.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void RemoveTrack(int TrackId, int InstructorId)
        {
            var track = db.Tracks.FirstOrDefault(a => a.TrackId == TrackId);
            var instructor = db.Instructors.FirstOrDefault(b => b.UserId == InstructorId);

            if (track != null && instructor != null)
            {
                track.Instructors.Remove(instructor); 
                db.SaveChanges();
            }
        }
        public List<Track> GetInstructorTracks(int InstructorId)
        {
			var instructor = db.Instructors.FirstOrDefault(a => a.UserId == InstructorId);
			if (instructor != null)
            {
				return instructor.Tracks.ToList();
			}
			return null;
		}
        public bool IsSuperisor(int id)
        {
            var Tracks = db.Tracks.Where(a => a.SupervisorId == id).ToList();
            return Tracks.Count > 0;
            
        }
        public int GetTrackBySupervisor(int id)
        {
			var track = db.Tracks.FirstOrDefault(a => a.SupervisorId == id);
			if (track != null)
            {
				return track.TrackId;
			}
			return 0;
		}
    }
}
