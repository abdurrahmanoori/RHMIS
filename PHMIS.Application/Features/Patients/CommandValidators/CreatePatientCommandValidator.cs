using FluentValidation;
using PHMIS.Application.Features.Patients.Commands;

namespace Sigtas.Application.Features.TaxRole.MaintainEnterprise.CommandValidators;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator
        (/*IValidator<CorporationDto> corporationDtoValidator*/)
    {
        RuleFor(x => x.PatientCreateDto)
           .NotNull().WithMessage("Patient data is required.");

        When(x => x.PatientCreateDto != null, () =>
        {
            RuleFor(x => x.PatientCreateDto!.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.PatientCreateDto!.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.PatientCreateDto!.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.PatientCreateDto!.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .MaximumLength(20);

            RuleFor(x => x.PatientCreateDto!.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20);

            RuleFor(x => x.PatientCreateDto!.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be valid.")
                .MaximumLength(100);

            RuleFor(x => x.PatientCreateDto!.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200);
        });



        //this.RuleFor(temp => temp.taxPayerDto.ENTERPRISE)
        //    .NotNull().WithMessage("ENTERPRISE must not be null.")
        //    .DependentRules(() => // DependentRules ensures this block only runs if the previous rule is satisfied
        //    {
        //        this.RuleFor(ent => ent.taxPayerDto.ENTERPRISE)
        //        .SetValidator(enterpriseDtoValidator);

        //this.RuleForEach(estabTranl=>estabTranl.TaxPayerMaintainScrDto.ENTERPRISE)

        // Validate IND_ACTIVITIES only if INDIVIDUAL is not null
        //this.RuleFor(ind => ind.TaxPayerMaintainScrDto.ENTERPRISE.IND_ACTIVITY)
        //    .NotNull().WithMessage("IND_ACTIVITIES collection must not be null.");

        //this.RuleForEach(ind => ind.TaxPayerMaintainScrDto.INDIVIDUAL.IND_ACTIVITY)
        //    .SetValidator(new IndividualActivityDtoValidator());

        //// Apply IndividualDtoValidator to the INDIVIDUAL object
        //this.RuleFor(ind => ind.TaxPayerMaintainScrDto.INDIVIDUAL)
        //    .SetValidator(new IndividualDtoValidator());

       
    }



}