using AttendanceTrackingSystem.IRepository;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;

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



		public List<ActiveTrackWithStudentCount> GetActiveTracksWithStudentCount()
		{
			var activeTracks = db.Tracks.Where(track => track.IsActive).ToList();
			var activeTracksWithStudentCount = new List<ActiveTrackWithStudentCount>();
			foreach (var track in activeTracks)
			{
				var trackWithStudentCount = new ActiveTrackWithStudentCount
				{
					TrackId = track.TrackId,
					TrackName = track.Name,
					TrackCapacity = track.Capacity,
					StudentCount = db.Students.Count(student => student.TrackId == track.TrackId)
				};
				activeTracksWithStudentCount.Add(trackWithStudentCount);
			}
			return activeTracksWithStudentCount;
		}
		public Instructor getTrackSupervisor(int trackId)
		{
			int SuperID = db.Tracks.FirstOrDefault(a => a.TrackId == trackId).SupervisorId;
			return db.Instructors.FirstOrDefault(a => a.UserId == SuperID);
		}
	}
}
