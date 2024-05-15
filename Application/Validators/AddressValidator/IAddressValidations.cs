using Application.Dto.Adress;
using FluentValidation.Results;

namespace Application.Validators.AddressValidator
{
    public interface IAddressValidations
    {
        ValidationResult Validate(AddressDto addressDto);
    }

}