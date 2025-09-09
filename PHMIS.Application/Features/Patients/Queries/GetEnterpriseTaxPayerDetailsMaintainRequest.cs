//using MediatR;
//using Sigtas.Application.Common;
//using Sigtas.Application.Common.ValidationErrors;
//using Sigtas.Application.Contracts.Identity;
//using Sigtas.Application.Contracts.Persistence;
//using Sigtas.Application.DTOs.TaxRole.EnrollEnterPrise.MaintainEnterprise;
//using Sigtas.Application.Responses;


//namespace Sigtas.Application.Features.TaxRole.MaintainEnterprise.Queries
//{
//    public record GetEnterpriseTaxPayerDetailsMaintainRequest(long tinApplicationNo) :
//        IRequest<Result<EnterpriseTaxPayerMaintainDto>>
//    {
//    }

//    internal record GetEnterpriseTaxPayerDetailsMaintainRequestHandler :
//        IRequestHandler<GetEnterpriseTaxPayerDetailsMaintainRequest,
//            Result<EnterpriseTaxPayerMaintainDto>>
//    {
//        private readonly IUnitOfWork unitOfWork;
//        private readonly ICurrentUser currentUser;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="GetEnterpriseTaxPayerDetailsMaintainRequestHandler"/> class.
//        /// </summary>
//        /// <param name="unitOfWork">Unit Of Work Repository.</param>
//        /// <param name="mapper">Auto Mapper Object.</param>
//        public GetEnterpriseTaxPayerDetailsMaintainRequestHandler
//            (IUnitOfWork unitOfWork, ICurrentUser currentUser)
//        {
//            this.unitOfWork = unitOfWork;
//            this.currentUser = currentUser;
//        }

//        /// <inheritdoc/>
//        public async Task<Result<EnterpriseTaxPayerMaintainDto>>
//            Handle(GetEnterpriseTaxPayerDetailsMaintainRequest request, CancellationToken cancellationToken)
//        {
//            var lanNo = await this.currentUser.GetCurrentUserLangId();

//            var result = await this.unitOfWork.TaxPayerRepository
//                .GetEnterpriseTaxPayerTinApplicationDetailsMaintain(request.tinApplicationNo, lanNo);

//            if (request is null)
//            {
//                return Result<EnterpriseTaxPayerMaintainDto>
//                    .FailureResult(
//                    AppError.TinApplicaionNotFound.Code, "The Enterprise Tin application for the maintain enterprise was not found.");
//            }

//            return Result<EnterpriseTaxPayerMaintainDto>.SuccessResult(result);
//        }
//    }
//}