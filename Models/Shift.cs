using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Shift
    {
        public string Id { get; set; }

        public string ShiftCode { get; set; }

        public DateTime Day{ get; set; }

        [ForeignKey("Nurse")]
        public string? NurseId { get; set; }
        public Nurse? Nurse { get; set; }

        [ForeignKey("Reception")]
        public string? ReceptionId { get; set; }
        public Reception? Reception { get; set; }



    }
}
