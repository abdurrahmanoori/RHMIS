using PHMIS.Application.Repositories.Laboratory;
using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Repositories.Base;

namespace PHMIS.Infrastructure.Repositories.Laboratory
{
    public class LabTestGroupRepository : GenericRepository<LabTestGroup>, ILabTestGroupRepository
    {
        public LabTestGroupRepository(AppDbContext context) : base(context) { }
    }
}
