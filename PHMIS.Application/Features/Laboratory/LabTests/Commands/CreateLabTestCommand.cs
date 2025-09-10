using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Laboratory;
using PHMIS.Domain.Entities.Laboratory;

namespace PHMIS.Application.Features.Laboratory.LabTests.Commands
{
    public record CreateLabTestCommand(LabTestCreateDto LabTestCreateDto) : IRequest<Result<LabTestCreateDto>>;

    internal sealed class CreateLabTestCommandHandler : IRequestHandler<CreateLabTestCommand, Result<LabTestCreateDto>>
    {
        private readonly ILabTestRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLabTestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILabTestRepository repo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Result<LabTestCreateDto>> Handle(CreateLabTestCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<LabTest>(request.LabTestCreateDto);
            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);
            return Result<LabTestCreateDto>.SuccessResult(request.LabTestCreateDto);
        }
    }
}
