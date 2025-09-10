using PHMIS.Application.Repositories.Laboratory;
using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Repositories.Base;

namespace PHMIS.Infrastructure.Repositories.Laboratory
{
    public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(AppDbContext context) : base(context) { }
    }
}
