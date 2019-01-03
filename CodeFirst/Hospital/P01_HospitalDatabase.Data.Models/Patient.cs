using System.Collections.Generic;

namespace HospitalDatabase.Data.Models
{
    public class Patient
    {

        public Patient()
        {
            this.Prescriptions = new List<PatientMedicament>();
            this.Diagnoses = new List<Diagnose>();
            this.Visitations = new List<Visitation>();
        }
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Addreess { get; set; }

        public string Email { get; set; }

        public bool hasInsurance { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

    }
}
