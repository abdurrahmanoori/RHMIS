using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Laboratory;

namespace PHMIS.Application.Features.LabTestGroups.Commands
{
    public record DeleteLabTestGroupCommand(int Id) : IRequest<Result<Unit>>;

    internal sealed class DeleteLabTestGroupCommandHandler : IRequestHandler<DeleteLabTestGroupCommand, Result<Unit>>
    {
        private readonly ILabTestGroupRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLabTestGroupCommandHandler(IUnitOfWork unitOfWork, ILabTestGroupRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        public async Task<Result<Unit>> Handle(DeleteLabTestGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return Result<Unit>.NotFoundResult(request.Id);
            }

            await _repo.RemoveAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}
