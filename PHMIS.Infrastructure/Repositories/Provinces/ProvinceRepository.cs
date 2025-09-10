using PHMIS.Application.Repositories.Provinces;
using PHMIS.Domain.Entities;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Repositories.Base;

namespace PHMIS.Infrastructure.Repositories.Provinces
{
    public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
