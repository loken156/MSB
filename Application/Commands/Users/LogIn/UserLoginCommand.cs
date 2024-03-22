﻿using Application.Dto.LogIn;
using Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.LogIn
{
    public class UserLoginCommand : IRequest<UserModel>
    {
        public LogInDto logInDtos { get; set; }

        public UserLoginCommand(LogInDto logInDto)
        {
            logInDtos = logInDto;
        }
    }

}

