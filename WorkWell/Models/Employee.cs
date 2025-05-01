using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWell.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public int SickLeaves { get; set; }
        public int CasualLeaves { get; set; }
        public int VacationLeaves { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Payroll> sPayroll { get; set; }



    }
}
