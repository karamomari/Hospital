

namespace Hospital.Models
{
    public class Doctor
    {
      
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
