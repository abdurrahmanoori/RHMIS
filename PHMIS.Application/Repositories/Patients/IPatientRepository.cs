using PHMIS.Application.Repositories.Base;
using PHMIS.Domain.Entities.Patients;

namespace PHMIS.Application.Repositories.Patients
{
    public  interface IPatientRepository : IGenericRepository<Patient>
    {
    }
}
