using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWell.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual HRManager HRManager { get; set; }
    }
}
