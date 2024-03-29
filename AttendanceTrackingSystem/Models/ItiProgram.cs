namespace AttendanceTrackingSystem.Models
{
    public enum ProgramName
    {
        PTP,
        ITP,
        STP
    }
    public class ItiProgram
    {
        public int ItiProgramId { get; set; }
        public ProgramName Name { get; set; }
        public int Duration { get; set; }

    }
}
