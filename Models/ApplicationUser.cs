
namespace Hospital.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? AccountCreatedAt { get; set; }
        public string? LastTokenGenerated { get; set; }
        public bool IsActive { get; set; }

        public string? ImageURL { get; set; }

        public List<Nurse> Nurses { get; set; }

        public List<Doctor> Doctors { get; set; }
        public List<Reception> Receptions { get; set; }

        public List<Patient> Patients { get; set; }


    }
}
