
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hospital.Models
{
    public class MvcHospitalcontext:IdentityDbContext<ApplicationUser>
    {

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }


        public MvcHospitalcontext(DbContextOptions<MvcHospitalcontext> options):base(options)
        {
            
        }
    }
}
