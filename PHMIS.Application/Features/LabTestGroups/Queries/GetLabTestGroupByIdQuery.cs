using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Laboratory;

namespace PHMIS.Application.Features.LabTestGroups.Queries
{
    public record GetLabTestGroupByIdQuery(int Id) : IRequest<Result<LabTestGroupDto>>;

    internal sealed class GetLabTestGroupByIdQueryHandler : IRequestHandler<GetLabTestGroupByIdQuery, Result<LabTestGroupDto>>
    {
        private readonly ILabTestGroupRepository _repo;
        private readonly IMapper _mapper;

        public GetLabTestGroupByIdQueryHandler(ILabTestGroupRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<LabTestGroupDto>> Handle(GetLabTestGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return Result<LabTestGroupDto>.NotFoundResult(request.Id);
            }

            var dto = _mapper.Map<LabTestGroupDto>(entity);
            return Result<LabTestGroupDto>.SuccessResult(dto);
        }
    }
}
