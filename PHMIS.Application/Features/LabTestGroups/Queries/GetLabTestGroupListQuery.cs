using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PHMIS.Application.Common;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Laboratory;

namespace PHMIS.Application.Features.LabTestGroups.Queries
{
    public record GetLabTestGroupListQuery(int PageNumber = 1, int PageSize = 25) : IRequest<Result<PagedList<LabTestGroupDto>>>;

    internal sealed class GetLabTestGroupListQueryHandler : IRequestHandler<GetLabTestGroupListQuery, Result<PagedList<LabTestGroupDto>>>
    {
        private readonly ILabTestGroupRepository _repo;
        private readonly IMapper _mapper;

        public GetLabTestGroupListQueryHandler(ILabTestGroupRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<LabTestGroupDto>>> Handle(GetLabTestGroupListQuery request, CancellationToken cancellationToken)
        {
            var query = _repo.GetAllQueryable();
            var dtoQuery = query.ProjectTo<LabTestGroupDto>(_mapper.ConfigurationProvider);
            var paged = await dtoQuery.ToPagedList(request.PageNumber, request.PageSize);
            if (paged.Items.Count == 0)
            {
                return Result<PagedList<LabTestGroupDto>>.EmptyResult(nameof(LabTestGroupDto));
            }
            return Result<PagedList<LabTestGroupDto>>.SuccessResult(paged);
        }
    }
}
