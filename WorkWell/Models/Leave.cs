using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWell.Models
{
    class Leave
    {
        [Key]
        public int LeaveID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        public string LeaveType { get; set; }

        public DateOnly Date { get; set; }
        public string Status { get; set; }
        
    }
}
