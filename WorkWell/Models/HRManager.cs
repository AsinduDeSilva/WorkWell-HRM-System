using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWell.Models
{
    public class HRManager
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
