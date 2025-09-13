namespace PHMIS.Application.DTO.Hospitals
{
    public class HospitalDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? EmergencyPhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public PHMIS.Domain.Entities.HospitalType? Type { get; set; }
        public PHMIS.Domain.Entities.HospitalCategory? Category { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public bool IsActive { get; set; }
        public bool? EmergencyServices { get; set; }
        public int? TotalBeds { get; set; }
        public int? AvailableBeds { get; set; }
        public int? IcuBeds { get; set; }
        public int? EmergencyBeds { get; set; }
        public bool? HasPharmacy { get; set; }
        public bool? HasLaboratory { get; set; }
        public bool? HasRadiology { get; set; }
        public bool? HasOperationTheater { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
