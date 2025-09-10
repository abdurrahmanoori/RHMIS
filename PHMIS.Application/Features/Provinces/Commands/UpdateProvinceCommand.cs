using AutoMapper;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Provinces;

namespace PHMIS.Application.Features.Provinces.Commands
{
    public record UpdateProvinceCommand(int Id, ProvinceCreateDto ProvinceDto) : IRequest<Result<ProvinceDto>>;

    internal sealed class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand, Result<ProvinceDto>>
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProvinceCommandHandler(IUnitOfWork unitOfWork, IProvinceRepository provinceRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProvinceDto>> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            var existing = await _provinceRepository.GetByIdAsync(request.Id);
            if (existing is null)
            {
                return Result<ProvinceDto>.NotFoundResult(request.Id);
            }

            _mapper.Map(request.ProvinceDto, existing);
            await _unitOfWork.SaveChanges(cancellationToken);

            var response = _mapper.Map<ProvinceDto>(existing);
            return Result<ProvinceDto>.SuccessResult(response);
        }
    }
}
