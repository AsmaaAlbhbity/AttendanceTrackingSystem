using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModel;

namespace AttendanceTrackingSystem.IRepository
{
	public interface IRepoTrack
	{
		public List<Track> getAll();
		public Track getById(int id);
		public void Add(Track track);
		public void Update(Track track);
		public void Delete(int id);

		public List<ActiveTrackWithStudentCount> GetActiveTracksWithStudentCount();
		public Instructor getTrackSupervisor(int trackId);
	}
}
