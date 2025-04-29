using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWell.Models
{
    class HRManager
    {
        [Key]
        public int HRManagerID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string NIC { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
