using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repository
{
    public class RepoTrack : IRepoTrack
    {
        AttendanceDBContext db;
        public RepoTrack(AttendanceDBContext _db)
        {
            db = _db;
        }

        public void Add(Track track)
        {
            db.Tracks.Add(track);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = db.Tracks.FirstOrDefault(a => a.TrackId == id);
            db.Tracks.Remove(obj);
            db.SaveChanges();
        }

        public List<Track> getAll()
        {
            return db.Tracks.ToList();
        }

        public Track getById(int id)
        {
            return db.Tracks.FirstOrDefault(a => a.TrackId == id);
        }

        public void Update(Track track)
        {
            db.Tracks.Update(track);
            db.SaveChanges();
        }

        public List<Track> GetActiveTracks()
        {
            return db.Tracks.Where(track => track.IsActive==true).ToList();
        }
    }
}
