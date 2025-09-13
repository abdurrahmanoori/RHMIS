using MediatR;
using AutoMapper;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Hospitals;
using PHMIS.Application.Repositories.Hospitals;

namespace PHMIS.Application.Features.Hospitals.Queries
{
    public record GetHospitalByIdQuery(int Id) : IRequest<Result<HospitalDto>>;

    internal sealed class GetHospitalByIdQueryHandler : IRequestHandler<GetHospitalByIdQuery, Result<HospitalDto>>
    {
        private readonly IHospitalRepository _repo;
        private readonly IMapper _mapper;

        public GetHospitalByIdQueryHandler(IHospitalRepository repo, IMapper mapper)
        { _repo = repo; _mapper = mapper; }

        public async Task<Result<HospitalDto>> Handle(GetHospitalByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity == null) return Result<HospitalDto>.NotFoundResult(request.Id);
            return Result<HospitalDto>.SuccessResult(_mapper.Map<HospitalDto>(entity));
        }
    }
}
