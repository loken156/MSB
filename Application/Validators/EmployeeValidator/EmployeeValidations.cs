﻿using Application.Dto.Employee;
using FluentValidation;

namespace Application.Validators.EmployeeValidator
{
    public class EmployeeValidations : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidations()
        {
            // Validation for FirstName
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First Name cannot be empty")
                .MaximumLength(50).WithMessage("First Name can't be longer than 50 characters");

            // Validation for LastName
            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last Name cannot be empty")
                .MaximumLength(50).WithMessage("Last Name can't be longer than 50 characters");

            // Validation for Email
            RuleFor(employee => employee.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email address");

            // Validation for PhoneNumber
            RuleFor(employee => employee.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number cannot be empty")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits");

            // Validation for Department
            RuleFor(employee => employee.Department)
                .NotEmpty().WithMessage("Department cannot be empty");

            // Validation for Position
            RuleFor(employee => employee.Position)
                .NotEmpty().WithMessage("Position cannot be empty");

            // Validation for HireDate
            RuleFor(employee => employee.HireDate)
                .NotEmpty().WithMessage("Hire Date cannot be empty")
                .LessThan(DateTime.Now).WithMessage("Hire Date cannot be in the future");

            // Validation for WarehouseId
            RuleFor(employee => employee.WarehouseId)
                .NotEmpty().WithMessage("Warehouse ID cannot be empty");

            // Additional validations for roles
            RuleFor(employee => employee.Roles)
                .NotEmpty().WithMessage("Roles cannot be empty")
                .Must(r => r.Contains("Employee")).WithMessage("Roles must include 'Employee'");

            // Validation for Roles ensuring they have valid roles only
            RuleForEach(employee => employee.Roles)
                .NotEmpty().WithMessage("Role cannot be empty")
                .Must(role => role == "Employee" || role == "Manager" || role == "Admin")
                .WithMessage("Invalid role specified");

            // If additional fields like address details are required, add those validations here.
        }
    }
}
