using Application.Dto.Adress;
using Application.Dto.Register;
using FluentValidation;

// This class defines validation rules for RegisterDto objects using FluentValidation.
// Each property of the RegisterDto class is validated with specific rules:
// - Email must not be empty and must be in a correct email format.
// - Password must not be empty and must meet certain criteria:
//   - Minimum length of 5 characters.
//   - Maximum length of 15 characters.
//   - Must contain at least one uppercase letter, one lowercase letter, and one special character.
//   - Cannot be case-insensitive equal to "password".
// - The Address property is validated using the AddressValidations validator, which defines rules for AddressDto.  

namespace Application.Validators.UserValidator
{
    public class UserValidations : AbstractValidator<RegisterDto>
    {

        public UserValidations(IValidator<AddressDto> AddressValidations)
        {

            RuleFor(user => user.Email)
               .NotEmpty().WithMessage("EmployeeEmail is required.")
               .EmailAddress().WithMessage("EmployeeEmail is not in a correct format.");


            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password can not be empty")
                .MinimumLength(5).WithMessage("Minimum password length is 5 letters long")
                .MaximumLength(15).WithMessage("Maximum password length is 15 letters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character")
                .NotEqual("password", StringComparer.OrdinalIgnoreCase)
                .WithMessage("password cannot be password.");



            RuleFor(x => x.Address).SetValidator(AddressValidations);

        }
    }
}