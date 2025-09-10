using FluentValidation;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Repositories.Laboratory;

namespace PHMIS.Application.Features.Laboratory.LabTests.CommandValidators
{
    public class CreateLabTestCommandValidator : AbstractValidator<LabTestCreateDto>
    {
        public CreateLabTestCommandValidator(ILabTestGroupRepository groupRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).When(x => x.Price.HasValue)
                .WithMessage("Price must be non-negative.");

            RuleFor(x => x.LabTestGroupId)
                .Must(id => groupRepository.GetAllQueryable().Any(g => g.Id == id))
                .WithMessage("LabTestGroupId must reference an existing LabTestGroup.");
        }
    }
}
