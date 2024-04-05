using Application.Dto.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.EmployeeValidator
{
    public class EmployeeValidations : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidations()
        {
            RuleFor(employee => employee.EmployeeId)
                .NotEmpty().WithMessage("Employee Id cannot be empty");

            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First Name cannot be empty")
                .MaximumLength(50).WithMessage("First Name can't be longer than 50 characters");

            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last Name cannot be empty")
                .MaximumLength(50).WithMessage("Last Name can't be longer than 50 characters");

            RuleFor(employee => employee.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(employee => employee.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number cannot be empty")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits");

            RuleFor(employee => employee.JobTitle)
                .NotEmpty().WithMessage("Job Title cannot be empty")
                .MaximumLength(50).WithMessage("Job Title can't be longer than 50 characters");


        }



    }
}
