using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Provinces;

namespace PHMIS.Application.Features.Provinces.Commands
{
    public record DeleteProvinceCommand(int Id) : IRequest<Result<Unit>>;

    internal sealed class DeleteProvinceCommandHandler : IRequestHandler<DeleteProvinceCommand, Result<Unit>>
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProvinceCommandHandler(IUnitOfWork unitOfWork, IProvinceRepository provinceRepository)
        {
            _unitOfWork = unitOfWork;
            _provinceRepository = provinceRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _provinceRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return Result<Unit>.NotFoundResult(request.Id);
            }

            await _provinceRepository.RemoveAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}
