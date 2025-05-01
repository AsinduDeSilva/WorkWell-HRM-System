using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWell.Models
{
    class Department
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
