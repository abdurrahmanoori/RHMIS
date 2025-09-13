using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Hospitals;

namespace PHMIS.Application.Features.Hospitals.Commands
{
    public record DeleteHospitalCommand(int Id) : IRequest<Result<Unit>>;

    internal sealed class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand, Result<Unit>>
    {
        private readonly IHospitalRepository _repo;
        private readonly IUnitOfWork _uow;

        public DeleteHospitalCommandHandler(IHospitalRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }

        public async Task<Result<Unit>> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null) return Result<Unit>.NotFoundResult(request.Id);

            await _repo.RemoveAsync(existing);
            await _uow.SaveChanges(cancellationToken);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}
