using System.ComponentModel.DataAnnotations;

namespace PHMIS.Domain.Entities.Patients
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string PublicId { get; set; } = System.Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
