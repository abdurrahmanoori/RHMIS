using PHMIS.Domain.Common;
using PHMIS.Domain.Entities.Identity.Entity;
using PHMIS.Domain.Entities.Patients;

namespace PHMIS.Domain.Entities
{
    public class Hospital : BaseAuditableEntity
    {
        public string Code { get; set; } = string.Empty; // Internal code
        public string? LicenseNumber { get; set; }
        public string? TaxIdentificationNumber { get; set; }

        // Contact Information
        public string Phone { get; set; } = string.Empty;
        public string? EmergencyPhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Website { get; set; }

        // Address
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        // Operational Details
        public HospitalType? Type { get; set; } // Enum: Public, Private, Teaching, etc.
        public HospitalCategory? Category { get; set; } // Enum: General, Specialty, Clinic, etc.
        public DateTime? EstablishedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool? EmergencyServices { get; set; }

        // Capacity & Facilities
        public int? TotalBeds { get; set; }
        public int? AvailableBeds { get; set; }
        public int? IcuBeds { get; set; }
        public int? EmergencyBeds { get; set; }
        public bool? HasPharmacy { get; set; }
        public bool? HasLaboratory { get; set; }
        public bool? HasRadiology { get; set; }
        public bool? HasOperationTheater { get; set; }

        // Administrative
        //public string? DirectorName { get; set; }
        //public string? MedicalDirectorName { get; set; }
        //public string? AdminContactPerson { get; set; }
        //public string? AdminContactPhone { get; set; }

        // Geo Location (for maps/distance calculations)
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

          // Navigation Properties
        //public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
        public virtual ICollection<ApplicationUser>? Doctors { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<Patient>? Patients { get; set; } = new List<Patient>();
        //public virtual ICollection<HospitalFacility> Facilities { get; set; } = new List<HospitalFacility>();
    }
    public enum HospitalType
    {
        Public = 1,
        Private = 2,
        Teaching = 3,
        Research = 4,
        Military = 5,
        Charity = 6
    }

    public enum HospitalCategory
    {
        General = 1,
        Specialty = 2,        // Cardiac, Cancer, etc.
        MultiSpecialty = 3,
        Clinic = 4,
        DiagnosticCenter = 5,
        Maternity = 6,
        Rehabilitation = 7
    }

    public enum HospitalStatus
    {
        Active = 1,
        Inactive = 2,
        UnderMaintenance = 3,
        Closed = 4
    }
}
