using MediatR;
using AutoMapper;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Hospitals;
using PHMIS.Application.Repositories.Hospitals;
using PHMIS.Application.Repositories.Base;
using PHMIS.Domain.Entities;

namespace PHMIS.Application.Features.Hospitals.Commands
{
    public record CreateHospitalCommand(HospitalCreateDto Hospital) : IRequest<Result<HospitalDto>>;

    internal sealed class CreateHospitalCommandHandler : IRequestHandler<CreateHospitalCommand, Result<HospitalDto>>
    {
        private readonly IHospitalRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateHospitalCommandHandler(IHospitalRepository repo, IUnitOfWork uow, IMapper mapper)
        {
            _repo = repo; _uow = uow; _mapper = mapper;
        }

        public async Task<Result<HospitalDto>> Handle(CreateHospitalCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Hospital>(request.Hospital);
            await _repo.AddAsync(entity);
            await _uow.SaveChanges(cancellationToken);
            var dto = _mapper.Map<HospitalDto>(entity);
            return Result<HospitalDto>.SuccessResult(dto);
        }
    }
}
