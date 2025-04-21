using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Nurse
    {
        public string Id { get; set; }
        public string Department { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public List<Shift> Shifts { get; set; }

    }
}
