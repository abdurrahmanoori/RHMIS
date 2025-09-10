using PHMIS.Application.Repositories.Patients;
using PHMIS.Domain.Entities.Patients;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Repositories.Base;

namespace PHMIS.Infrastructure.Repositories.Patients
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
