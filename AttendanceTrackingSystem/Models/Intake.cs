namespace AttendanceTrackingSystem.Models
{
    public class Intake
    {
        public int IntakeId { get; set; }
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public int ItiProgramId { get; set; }
        public virtual ItiProgram Program { get; set; }
    }
}
