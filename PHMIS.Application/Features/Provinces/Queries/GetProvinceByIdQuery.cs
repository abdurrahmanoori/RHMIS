using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Application.Repositories.Provinces;

namespace PHMIS.Application.Features.Provinces.Queries
{
    public record GetProvinceByIdQuery(int Id) : IRequest<Result<ProvinceDto>>;

    internal sealed class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceByIdQuery, Result<ProvinceDto>>
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;

        public GetProvinceByIdQueryHandler(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProvinceDto>> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _provinceRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return Result<ProvinceDto>.NotFoundResult(request.Id);
            }

            var dto = _mapper.Map<ProvinceDto>(entity);
            return Result<ProvinceDto>.SuccessResult(dto);
        }
    }
}
