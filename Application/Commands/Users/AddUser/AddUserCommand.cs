using Application.Dto.Register;
using Infrastructure.Entities;
using MediatR;

public class AddUserCommand : IRequest<ApplicationUser>
{
    public RegisterDto RegisterData { get; }

    public AddUserCommand(RegisterDto registerData)
    {
        RegisterData = registerData;
    }
}
