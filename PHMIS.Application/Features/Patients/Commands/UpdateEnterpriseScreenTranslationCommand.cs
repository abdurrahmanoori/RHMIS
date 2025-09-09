//using AutoMapper;
//using MediatR;
//using Sigtas.Application.Contracts.Persistence;
//using Sigtas.Application.DTOs.TaxRole.EnrollEnterPrise.MaintainEnterprise;
//using Sigtas.Application.DTOs.TaxRole.EnrollIndividual.MaintainScreen;
//using Sigtas.Application.Responses;
//using Sigtas.Domain.Entities.TaxRole.Maintain;

//namespace Sigtas.Application.Features.TaxRole.MaintainEnterprise.Commands
//{
//    public record UpdateEnterpriseScreenTranslationCommand(EnterpriseScreenTranslationDto? TranslationDto)
//        : IRequest<Result<EnterpriseScreenTranslationDto>>;

//    internal sealed class UpdateEnterpriseScreenTranslationCommandHandler
//        : IRequestHandler<UpdateEnterpriseScreenTranslationCommand, Result<EnterpriseScreenTranslationDto>>
//    {
//        private readonly IUnitOfWork unitOfWork;
//        private readonly IMapper mapper;

//        public UpdateEnterpriseScreenTranslationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            this.unitOfWork = unitOfWork;
//            this.mapper = mapper;
//        }

//        public async Task<Result<EnterpriseScreenTranslationDto>> Handle(UpdateEnterpriseScreenTranslationCommand request,
//            CancellationToken cancellationToken)
//        {
//            if (request.TranslationDto is not null)
//            {
//                await UpdateTranslationsAsync(request.TranslationDto);
//                await unitOfWork.SaveChangesSequenceAutoGenerator(cancellationToken);
//            }

//            return Result<EnterpriseScreenTranslationDto>.SuccessResult(request.TranslationDto);
//        }

//        private async Task UpdateTranslationsAsync(EnterpriseScreenTranslationDto dto)
//        {
//            if (dto is null) return;

//            if (dto.TAXPYR_TRANSL.TAX_PAYER_NO > 0 && dto.TAXPYR_TRANSL.LANG_NO > 0)
//            {
//                var taxPayerTranslation = await this.unitOfWork.TaxPayerTranslationRepository
//                    .GetFirstOrDefaultAsync(x => x.TAX_PAYER_NO == dto.TAXPYR_TRANSL.TAX_PAYER_NO, true);

//                if (taxPayerTranslation is not null)
//                {
//                    this.mapper.Map<TaxPayerTranslationDto, TAXPYR_TRANSL>(dto.TAXPYR_TRANSL, taxPayerTranslation);
//                }
//            }

//            if (dto.ENTP_TRANSL.ENTERPRISE_NO > 0 && dto.ENTP_TRANSL.LANG_NO > 0)
//            {
//                var enterpriseTranslation = await this.unitOfWork.EnterpriseTranslationRepository
//                    .GetFirstOrDefaultAsync(x => x.ENTERPRISE_NO == dto.ENTP_TRANSL.ENTERPRISE_NO,true);

//                if (enterpriseTranslation is not null)
//                {
//                    this.mapper.Map(dto.ENTP_TRANSL, enterpriseTranslation);
//                }
//            }

//            if (dto.ESTAB_TRANSL.ESTAB_NO > 0 && dto.ESTAB_TRANSL.LANG_NO > 0)
//            {
//                var establishmentTranslation = await this.unitOfWork.EstablishmentTranslationRepository
//                    .GetFirstOrDefaultAsync(x => x.ESTAB_NO == dto.ESTAB_TRANSL.ESTAB_NO, true);

//                if (establishmentTranslation is not null)
//                {
//                    this.mapper.Map(dto.ESTAB_TRANSL, establishmentTranslation);
//                }
//            }
//        }
//    }
//}










////namespace Sigtas.Application.Features.SIGTAS.TaxRole.MaintainEnterprise.Command
////{
////    using AutoMapper;
////    using MediatR;
////    using Sigtas.Application.Contracts.Identity;
////    using Sigtas.Application.Contracts.Persistence;
////    using Sigtas.Application.DTOs.SIGTAS.TaxRole.EnrollEnterprise.MaintainEnterprise;
////    using Sigtas.Application.Responses;
////    using Sigtas.Domain.Entities.SIGTAS.TaxRole;
////    using Sigtas.Domain.Entities.SIGTAS.TaxRole.Maintain;

////    public record UpdateEnterpriseScreenTranslationCommand(EnterpriseScreenTranslationDto? TranslationDto) :
////            IRequest<Result<EnterpriseScreenTranslationDto>>
////    { }

////    internal sealed class UpdateEnterpriseScreenTranslationCommandHandler :
////        IRequestHandler<UpdateEnterpriseScreenTranslationCommand, Result<EnterpriseScreenTranslationDto>>
////    {
////        private readonly IUnitOfWork unitOfWork;
////        private readonly IMapper mapper;

////        public UpdateEnterpriseScreenTranslationCommandHandler
////            (IUnitOfWork unitOfWork, IMapper mapper)
////        {
////            this.unitOfWork = unitOfWork;
////            this.mapper = mapper;
////        }

////        public async Task<Result<EnterpriseScreenTranslationDto>>
////            Handle(UpdateEnterpriseScreenTranslationCommand request, CancellationToken cancellationToken)
////        {
////            await AddTranslationIfAvaliableAsync(request.TranslationDto);
////            await this.unitOfWork.SaveChangesSequenceAutoGenerator(cancellationToken);

////            return Result<EnterpriseScreenTranslationDto>.SuccessResult(request.TranslationDto);
////        }
////        private async Task AddTranslationIfAvaliableAsync(EnterpriseScreenTranslationDto? translationDto)
////        {

////            if (translationDto?.TAXPYR_TRANSL is not null)
////            {
////                await this.unitOfWork.TaxPayerTranslationRepository
////                    .AddAsync(this.mapper.Map<TAXPYR_TRANSL>(translationDto.TAXPYR_TRANSL));
////            }
////            if (translationDto?.ENTP_TRANSL is not null)
////            {
////                await this.unitOfWork.EnterpriseTranslationRepository
////                    .AddAsync(this.mapper.Map<ENTP_TRANSL>(translationDto.ENTP_TRANSL));
////            }
////            if (translationDto?.ESTAB_TRANSL is not null)
////            {
////                await this.unitOfWork.EstablishmentTranslationRepository
////                    .AddAsync(this.mapper.Map<ESTAB_TRANSL>(translationDto.ESTAB_TRANSL));
////            }
////        }

////    }
////}