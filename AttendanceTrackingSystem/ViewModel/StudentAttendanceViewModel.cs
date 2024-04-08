using System;
using System.Collections.Generic;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModels
{
    public class StudentAttendanceViewModel
    {
        public DateTime SelectedDate { get; set; }
        public int SelectedTrackId { get; set; }
        public List<Attendance> StudentAttendances { get; set; }
        public List<Track> Tracks { get; set; }
        public Dictionary<AttendaneStatus, List<Attendance>> StudentAttendanceDictionary { get; set; }

        public List<Permission> Permissions { get; set; } 

        // Constructor to initialize the properties
        public StudentAttendanceViewModel()
        {
            Tracks = new List<Track>();
            StudentAttendances = new List<Attendance>();
            StudentAttendanceDictionary = new Dictionary<AttendaneStatus, List<Attendance>>();
            Permissions = new List<Permission>();


        }
    }
}
