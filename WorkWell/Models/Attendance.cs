using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWell.Models
{
    public class Attendance
    {

        [Key]
        public int AttendanceID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        public TimeOnly? CheckInTime { get; set; }
        public TimeOnly? CheckOutTime { get; set; }

    }
}
