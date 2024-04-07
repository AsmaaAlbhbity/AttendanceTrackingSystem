namespace AttendanceTrackingSystem.ViewModel
{
	public class InstructorEditProfileViewModel : EditProfileViewModel
	{
		public decimal InstructorSalary { get; set; }
		public string TrackNames { get; set; }
		public string? SupervisedTrack { get; set; }
	}
}
