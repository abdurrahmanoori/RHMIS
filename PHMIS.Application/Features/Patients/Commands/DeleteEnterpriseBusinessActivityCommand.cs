//using MediatR;
//using Sigtas.Application.Common;
//using Sigtas.Application.Common.ValidationErrors;
//using Sigtas.Application.Contracts.Persistence;
//using Sigtas.Application.Responses;
//using Sigtas.Domain.Entities.Administration.EnterpriseActivity;
//using Sigtas.Domain.Entities.TaxRole.FiscalPeriod;


//namespace Sigtas.Application.Features.TaxRole.MaintainEnterprise.Commands
//{
//    public record DeleteEnterpriseBusinessActivityCommand(byte EnterpriseBusinessActivityNo,long enterpriseNo) : IRequest<Result<bool>> { }

//    internal sealed class DeleteEnterpriseBusinessActivityCommandHandler :
//        IRequestHandler<DeleteEnterpriseBusinessActivityCommand, Result<bool>>
//    {
//        private readonly IUnitOfWork unitOfWork;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DeleteEnterpriseBusinessActivityCommandHandler"/> class.
//        /// </summary>
//        /// <param name="unitOfWork"></param>
//        /// <param name="mapper"></param>
//        public DeleteEnterpriseBusinessActivityCommandHandler(IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//        }

//        /// <inheritdoc/>
//        public async Task<Result<bool>> Handle
//            (DeleteEnterpriseBusinessActivityCommand request, CancellationToken cancellationToken)
//        {
//            var EnterpriseBusinessActivity = await unitOfWork.CommonEnterpriseTaxPayerRepository
//                .GetAsync<ENT_BUS_ACT>(x=>x.ENT_ACTIVITY_NO == request.EnterpriseBusinessActivityNo 
//                && x.ENTERPRISE_NO == request.enterpriseNo);

//            if (EnterpriseBusinessActivity is null)
//            {
//                return Result<bool>
//                        .FailureResult(AppError.NotFound.Code,
//                        $"The {typeof(FISCAL_YEAR).Name} with the EnterpriseBusinessActivity_No {request.EnterpriseBusinessActivityNo} not found");
//            }

//            await unitOfWork.CommonEnterpriseTaxPayerRepository.RemoveAsync(EnterpriseBusinessActivity);
//            await unitOfWork.SaveChanges(cancellationToken);
//            return Result<bool>.SuccessResult(true);
//        }
//    }
//}
