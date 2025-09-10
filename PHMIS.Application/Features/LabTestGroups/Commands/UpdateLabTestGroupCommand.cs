using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Laboratory;

namespace PHMIS.Application.Features.LabTestGroups.Commands
{
    public record UpdateLabTestGroupCommand(int Id, LabTestGroupCreateDto LabTestGroupDto) : IRequest<Result<LabTestGroupDto>>;

    internal sealed class UpdateLabTestGroupCommandHandler : IRequestHandler<UpdateLabTestGroupCommand, Result<LabTestGroupDto>>
    {
        private readonly ILabTestGroupRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLabTestGroupCommandHandler(IUnitOfWork unitOfWork, ILabTestGroupRepository repo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<LabTestGroupDto>> Handle(UpdateLabTestGroupCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                return Result<LabTestGroupDto>.NotFoundResult(request.Id);
            }

            _mapper.Map(request.LabTestGroupDto, existing);
            await _unitOfWork.SaveChanges(cancellationToken);

            var response = _mapper.Map<LabTestGroupDto>(existing);
            return Result<LabTestGroupDto>.SuccessResult(response);
        }
    }
}
