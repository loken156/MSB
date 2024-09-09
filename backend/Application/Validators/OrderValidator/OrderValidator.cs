using Application.Dto.Order;
using FluentValidation;

// This class defines validation rules for OrderDto objects using FluentValidation.
// Each property of the OrderDto class is validated with specific rules:
// - OrderId must not be empty.
// - OrderDate must not be empty.
// - TotalCost must not be empty and must be greater than 0.
// - OrderStatus must not be empty and can't exceed 50 characters.
// - UserId must not be empty.
// - RepairNotes can't exceed 200 characters.
// Additional validations for other fields can be added as needed.

namespace Application.Validators.OrderValidator
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(order => order.OrderId)
                .NotEmpty().WithMessage("Order Id cannot be empty");

            RuleFor(order => order.OrderDate)
                .NotEmpty().WithMessage("Order Date cannot be empty");

            RuleFor(order => order.TotalCost)
                .NotEmpty().WithMessage("Total Cost cannot be empty")
                .GreaterThan(0).WithMessage("Total Cost must be greater than 0");

            RuleFor(order => order.OrderStatus)
                .NotEmpty().WithMessage("Order Status cannot be empty")
                .MaximumLength(50).WithMessage("Order Status can't be longer than 50 characters");

            RuleFor(order => order.UserId)
                .NotEmpty().WithMessage("User Id cannot be empty");

            //RuleFor(order => order.AddressId)
            //    .NotEmpty().WithMessage("Address Id cannot be empty");

            RuleFor(order => order.RepairNotes)
                .MaximumLength(200).WithMessage("Repair Notes can't be longer than 200 characters");
        }
    }
}