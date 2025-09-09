using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Repositories.Patients;

namespace PHMIS.Application.Features.Patients.Queries
{
    public record GetPatientByIdQuery(int Id) : IRequest<Result<PatientDto>> { }

    internal sealed class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Result<PatientDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);
            if (patient is null)
            {
                return Result<PatientDto>.NotFoundResult(request.Id);
            }

            var dto = _mapper.Map<PatientDto>(patient);
            return Result<PatientDto>.SuccessResult(dto);
        }
    }
}
