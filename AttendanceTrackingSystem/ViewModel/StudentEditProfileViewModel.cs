namespace AttendanceTrackingSystem.ViewModel
{
	public class StudentEditProfileViewModel : EditProfileViewModel
	{
		public int StudentDegree { get; set; }
		public string StudentUniversity { get; set; }
		public string StudentFaculity { get; set; }
		public DateTime StudentGraduationYear { get; set; }
		public string StudentSpecialization { get; set; }
		public int TrackId { get; set; }
		public string TrackName { get; set; }
		public string TrackSupervisor { get; set; }
	}
}
