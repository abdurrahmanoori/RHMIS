using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Patients;
using PHMIS.Domain.Entities;
using PHMIS.Identity.IServices;


namespace PHMIS.Application.Features.Patients.Commands
{
    public record CreatePatientCommand(PatientCreateDto? PatientCreateDto) : IRequest<Result<PatientCreateDto>>
    { }

    internal sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<PatientCreateDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public CreatePatientCommandHandler
            (IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser, IPatientRepository patientRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _patientRepository = patientRepository;
        }

        public async Task<Result<PatientCreateDto>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientEntity = _mapper.Map<Patient>(request.PatientCreateDto);
            await _patientRepository.AddAsync(patientEntity);
            await _unitOfWork.SaveChanges(cancellationToken);

            return Result<PatientCreateDto>
                .SuccessResult(request.PatientCreateDto);
        }
        //private async Task<(TAX_PAYER taxPayer, PatientCreateDto taxPayerDto)> InitializeTaxPayerValues(PatientCreateDto? taxPayerDto)
        //{

        //    long taxPayerNo = await this.unitOfWork.GetSequenceValueAsync<TAX_PAYER>().ConfigureAwait(false);
        //    this.SetTaxPayerDefaults(taxPayerDto, taxPayerNo);

        //    await Task.WhenAll(
        //    this.InitializeFiscalYears(taxPayerDto, taxPayerNo),
        //    this.InitializeTaxAccount(taxPayerDto, taxPayerNo),
        //    //this.InitializeTaxPayerTranslations(taxPayerDto, taxPayerNo),
        //    this.InitializeBankAccounts(taxPayerDto, taxPayerNo),
        //    this.InitializeCaseManagementOfficers(taxPayerDto, taxPayerNo),
        //    this.InitializeTaxPayerTaxType(taxPayerDto, taxPayerNo)
        //    ).ConfigureAwait(false);

        //    this.InitializeCorporation(taxPayerDto);
        //    await this.InitializeEnterprise(taxPayerDto, taxPayerNo);


        //    var taxPayer = this.mapper.Map<TAX_PAYER>(taxPayerDto);






        //}
    }
}