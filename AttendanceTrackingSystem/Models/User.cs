using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AttendanceTrackingSystem.Models
{
    public enum Approve
    {
        Accepted,
        pending,
        Fired

    }
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; }
        public Approve IsApproved { get; set; }
        public string UserType { get; set; }
        public string? ImgUrl { get; set; }
        [JsonIgnore]
        public virtual List<Msg> Msgs { get; set; }
    }
}
