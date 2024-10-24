﻿using Application.Dto.LogIn;
using FluentValidation;

namespace Application.Validators.UserValidator
{
    public class LogInDtoValidator : AbstractValidator<LogInDto>
    {
        public LogInDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("EmployeeEmail can not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty");
        }
    }
}