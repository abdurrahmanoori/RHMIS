using PHMIS.Application.Repositories.Hospitals;
using PHMIS.Domain.Entities;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Repositories.Base;

namespace PHMIS.Infrastructure.Repositories.Hospitals
{
    public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
    {
        public HospitalRepository(AppDbContext context) : base(context)
        {
        }
    }
}
