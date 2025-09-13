namespace PHMIS.Application.Identity.Models
{
    public class UserCreateDto
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int HospitalId { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
