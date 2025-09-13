namespace PHMIS.Application.Identity.Models
{
    public class UserUpdateDto
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public int? HospitalId { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
