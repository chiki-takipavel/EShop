namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.Order.OrderName).NotEmpty().WithName("Name");
        RuleFor(c => c.Order.CustomerId).NotEmpty();
        RuleFor(c => c.Order.OrderItems).NotEmpty();
    }
}
