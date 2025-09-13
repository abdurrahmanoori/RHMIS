using MediatR;
using AutoMapper;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Hospitals;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Hospitals;

namespace PHMIS.Application.Features.Hospitals.Commands
{
    public record UpdateHospitalCommand(int Id, HospitalCreateDto Hospital) : IRequest<Result<HospitalDto>>;

    internal sealed class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand, Result<HospitalDto>>
    {
        private readonly IHospitalRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateHospitalCommandHandler(IHospitalRepository repo, IUnitOfWork uow, IMapper mapper)
        { _repo = repo; _uow = uow; _mapper = mapper; }

        public async Task<Result<HospitalDto>> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null) return Result<HospitalDto>.NotFoundResult(request.Id);

            _mapper.Map(request.Hospital, existing);
            if (string.IsNullOrWhiteSpace(existing.Name))
            {
                existing.Name = !string.IsNullOrWhiteSpace(existing.Code) ? existing.Code : (existing.Email ?? "Hospital");
            }

            await _uow.SaveChanges(cancellationToken);
            return Result<HospitalDto>.SuccessResult(_mapper.Map<HospitalDto>(existing));
        }
    }
}
