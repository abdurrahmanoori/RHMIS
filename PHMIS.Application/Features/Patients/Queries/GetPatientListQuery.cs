using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PHMIS.Application.Common;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Repositories.Patients;

namespace PHMIS.Application.Features.Patients.Queries
{
    public record GetPatientListQuery(int PageNumber = 1, int PageSize = 25) : IRequest<Result<PagedList<PatientDto>>> { }

    internal sealed class GetPatientListQueryHandler : IRequestHandler<GetPatientListQuery, Result<PagedList<PatientDto>>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientListQueryHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<PatientDto>>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
        {
            var query = _patientRepository.GetAllQueryable();
            var dtoQuery = query.ProjectTo<PatientDto>(_mapper.ConfigurationProvider);

            var paged = await dtoQuery.ToPagedList(request.PageNumber, request.PageSize);
            if (paged.Items.Any() == false)
            {
                return Result<PagedList<PatientDto>>.EmptyResult(nameof(PatientDto));
            }

            return Result<PagedList<PatientDto>>.SuccessResult(paged);
        }
    }
}
