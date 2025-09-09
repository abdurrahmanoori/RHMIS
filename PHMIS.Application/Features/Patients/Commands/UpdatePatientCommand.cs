using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Patients;
using PHMIS.Domain.Entities;

namespace PHMIS.Application.Features.Patients.Commands
{
    public record UpdatePatientCommand(int Id, PatientCreateDto PatientDto) : IRequest<Result<PatientDto>> { }

    internal sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork, IPatientRepository patientRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Result<PatientDto>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var existing = await _patientRepository.GetByIdAsync(request.Id);
            if (existing is null)
            {
                return Result<PatientDto>.NotFoundResult(request.Id);
            }

            // Map incoming values onto the tracked entity
            _mapper.Map(request.PatientDto, existing);
            //await _patientRepository.UpdateAsync(existing);
            await _unitOfWork.SaveChanges(cancellationToken);

            var response = _mapper.Map<PatientDto>(existing);
            return Result<PatientDto>.SuccessResult(response);
        }
    }
}
