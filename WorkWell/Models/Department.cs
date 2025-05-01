using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWell.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [ForeignKey(nameof(HRManager))]
        public int? HRManagerID { get; set; }
        public virtual HRManager HRManager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
