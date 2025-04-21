using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public DateTime DOB { get; set; }
        public string InsuranceNo { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
