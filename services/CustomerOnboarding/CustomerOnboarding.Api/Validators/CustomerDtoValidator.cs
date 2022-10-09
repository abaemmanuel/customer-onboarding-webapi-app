using CustomerOnboarding.ApplicationService.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can not be left empty")
                .NotNull().WithMessage("Phone number provider is invalid")
                .When(p => p.PhoneNumber.All(p => char.IsDigit(p)) == false)
                    .WithMessage("Phone number must only contain digits")
                .Length(11).WithMessage("Phone number must be 11 digits");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password can not be left empty")
                .NotNull().WithMessage("Password provided is invalid")
                .MinimumLength(8).WithMessage("Password should not be less than 8 characters")
                .Matches("^[a-zA-Z0-9@!]*$").WithMessage("Password should contain alphanumeric characters");

            RuleFor(c => c.StateOfResidence)
                .NotEmpty().WithMessage("State of residence can not be left empty")
                .NotNull().WithMessage("State of residence provided is invalid");

            RuleFor(c => c.LGA)
                .NotEmpty().WithMessage("Local government area can not be left empty")
                .NotNull().WithMessage("Local government area provided is invalid");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email address provided is invalid");
        }

    }
}
