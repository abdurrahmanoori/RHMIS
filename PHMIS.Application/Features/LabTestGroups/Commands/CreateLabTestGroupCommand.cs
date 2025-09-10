using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Laboratory;
using PHMIS.Domain.Entities.Laboratory;

namespace PHMIS.Application.Features.LabTestGroups.Commands
{
    public record CreateLabTestGroupCommand(LabTestGroupCreateDto LabTestGroupCreateDto) : IRequest<Result<LabTestGroupCreateDto>>;

    internal sealed class CreateLabTestGroupCommandHandler : IRequestHandler<CreateLabTestGroupCommand, Result<LabTestGroupCreateDto>>
    {
        private readonly ILabTestGroupRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLabTestGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILabTestGroupRepository repo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Result<LabTestGroupCreateDto>> Handle(CreateLabTestGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<LabTestGroup>(request.LabTestGroupCreateDto);
            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);
            return Result<LabTestGroupCreateDto>.SuccessResult(request.LabTestGroupCreateDto);
        }
    }
}
