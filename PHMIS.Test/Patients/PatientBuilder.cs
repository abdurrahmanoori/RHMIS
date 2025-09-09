using PHMIS.Application.DTO.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHMIS.Test.Patients
{
    public class PatientBuilder
    {
        private string _firstName = "John";
        private string _lastName = "Doe";
        private string _email = $"john.doe_{Guid.NewGuid():N}@test.com";

        public PatientBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public PatientBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public PatientCreateDto BuildCreateDto( ) => new()
        {
            FirstName = _firstName,
            LastName = _lastName,
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Male",
            PhoneNumber = "123-456-7890",
            Email = _email,
            Address = "123 Test St"
        };
    }
}
