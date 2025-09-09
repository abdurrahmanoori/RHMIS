using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PHMIS.Application.Common;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Application.Repositories.Provinces;

namespace PHMIS.Application.Features.Provinces.Queries
{
    public record GetProvinceListQuery(int PageNumber = 1, int PageSize = 25) : IRequest<Result<PagedList<ProvinceDto>>>;

    internal sealed class GetProvinceListQueryHandler : IRequestHandler<GetProvinceListQuery, Result<PagedList<ProvinceDto>>>
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;

        public GetProvinceListQueryHandler(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ProvinceDto>>> Handle(GetProvinceListQuery request, CancellationToken cancellationToken)
        {
            var query = _provinceRepository.GetAllQueryable();
            var dtoQuery = query.ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider);
            var paged = await dtoQuery.ToPagedList(request.PageNumber, request.PageSize);
            if (paged.Items.Any() == false)
            {
                return Result<PagedList<ProvinceDto>>.EmptyResult(nameof(ProvinceDto));
            }
            return Result<PagedList<ProvinceDto>>.SuccessResult(paged);
        }
    }
}
